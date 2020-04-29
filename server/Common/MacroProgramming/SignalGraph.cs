using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Common.Math;
using Common.Statistics;

namespace Common.MacroProgramming
{
    public class SignalGraph
    {
        private static readonly Color GridLineColor = Color.FromArgb(217, 234, 244);
        private static readonly Color ValueCurveColor = Color.FromArgb(54, 145, 198);
        private static readonly Color CorrelatedValueCurveColor = Color.LightGray; //Color.FromArgb(236, 222, 240);

        private static readonly Pen GridLinePen = new Pen(GridLineColor) {Width = 1f};
        private static readonly Pen ZeroLinePen = new Pen(Brushes.DarkBlue) {Width = 1f};
        private static readonly Pen ValueCurvePen = new Pen(ValueCurveColor) {Width = 2f};
        private static readonly Pen ValueLinePen = new Pen(Brushes.LightBlue) {Width = 1f};
        private static readonly Pen CorrelatedValueCurvePen = new Pen(CorrelatedValueCurveColor) {Width = 2f};
        private static readonly Pen MinValueLinePen = new Pen(Brushes.DarkRed) {Width = 1f};
        private static readonly Pen MaxValueLinePen = new Pen(Brushes.DarkRed) {Width = 1f};
        private static readonly Color AreaUnderTheCurveColor = Color.FromArgb(241, 246, 250);
        private static readonly Brush AreaUnderTheCurveBrush = new SolidBrush(AreaUnderTheCurveColor);
        private static readonly Brush ValueBrush = Brushes.Black;
        private static readonly Brush MinValueBrush = Brushes.LightPink;
        private static readonly Brush MaxValueBrush = Brushes.LightPink;
        private static readonly Brush ScaleFontBrush = Brushes.LightGray;
        private static readonly Brush FriendlyNameBrush = Brushes.Black;
        private static readonly Brush SubcollectionNameBrush = Brushes.Black;

        private static readonly Font XBigFont = new Font(FontFamily.GenericSansSerif, 18, FontStyle.Bold);
        private static readonly Font BigFont = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
        private static readonly Font MediumFont = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
        private static readonly Font SmallFont = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold);
        private static readonly Font SubcollectionNameFont = BigFont;
        private static readonly Font FriendlyNameFont = MediumFont;
        private static readonly Font ValueFont = MediumFont;
        private static readonly Font MinValueFont = SmallFont;
        private static readonly Font MaxValueFont = SmallFont;
        private static readonly Font ScaleFont = SmallFont;
        private TimeSpan _duration;
        private readonly Signal _signal;
        private readonly List<TimestampedDecimal> _signalStateHistory = new List<TimestampedDecimal>();
        private readonly object _signalStateHistoryLock = new object();
        private readonly DateTime _startTime = DateTime.UtcNow;

        public SignalGraph(Signal signal, int durationMs = 5000)
        {
            _signal = signal;
            _duration = TimeSpan.FromMilliseconds(durationMs);
            RegisterForChangedEvent(signal);
        }

        public void Draw(Graphics graphics, Rectangle targetRectangle)
        {
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //graphics.CompositingQuality = CompositingQuality.HighQuality;
            //graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //graphics.SmoothingMode = SmoothingMode.HighQuality;
            var originalClip = graphics.Clip;
            var originalTransform = graphics.Transform;

            var drawTime = DateTime.UtcNow;
            CaptureNewSample();
            var value = string.Empty;
            var rangeMinValue = string.Empty;
            var rangeMaxValue = string.Empty;
            const int topMarginHeight = 40;
            const int bottomMarginHeight = 20;
            var width = (float) targetRectangle.Width;
            var height = (float) targetRectangle.Height - topMarginHeight - bottomMarginHeight;
            graphics.SetClip(targetRectangle);
            graphics.Clear(Color.White);


            if (_signal is AnalogSignal || _signal is DigitalSignal)
            {
                graphics.TranslateTransform(-targetRectangle.Location.X, -targetRectangle.Location.Y);
                graphics.DrawRectangle(GridLinePen, 0, 0, targetRectangle.Width, targetRectangle.Height);
                graphics.TranslateTransform(0, topMarginHeight);
                graphics.DrawRectangle(GridLinePen, 0, 0, width, height);


                var x1 = 0.0f;
                var y1 = height / 2.0f;
                var correlatedY1 = height / 2.0f;

                var y2 = height / 2.0f;
                var correlatedY2 = height / 2.0f;

                var minY2 = 0.0f;
                var maxY2 = 0.0f;
                var minVal = 0.0;
                var maxVal = 0.0;
                var minValString = string.Empty;
                var maxValString = string.Empty;
                float zeroHeight = 0;
                var isFirstSample = true;
                const float numYSegments = 20.0f;
                const float numXSegments = 20.0f;
                var xOffset = -(float)(drawTime.Subtract(_startTime).TotalMilliseconds %
                                        (_duration.TotalMilliseconds / numXSegments) *
                                        (width / _duration.TotalMilliseconds));

                var valueCurvePointList = new List<PointF>();
                var correlatedValueCurvePointList = new List<PointF>();
                TimestampedDecimal[] signalStateHistory;
                lock (_signalStateHistoryLock)
                {
                    signalStateHistory = _signalStateHistory.ToArray();
                }
                foreach (var sample in signalStateHistory)
                {
                    var x2 = width - (float)(drawTime.Subtract(sample.Timestamp).TotalMilliseconds /
                                                _duration.TotalMilliseconds) * width;
                    if (x2 < 0) x2 = 0;
                    if (x2 > width) x2 = width;
                    if (_signal is DigitalSignal)
                    {
                        y2 = sample.Value == 1 ? 0 : height;
                        value = sample.Value == 1 ? "1" : "0";
                        rangeMinValue = "0";
                        rangeMaxValue = "1";
                        zeroHeight = height;
                        if (isFirstSample)
                        {
                            minVal = sample.Value;
                            maxVal = sample.Value;
                            minValString = sample.Value == 1 ? "1" : "0";
                            maxValString = sample.Value == 1 ? "1" : "0";
                        }
                        switch (sample.Value)
                        {
                            case 0:
                                minVal = 0;
                                minValString = "0";
                                break;
                            case 1:
                                maxVal = 1;
                                maxValString = "1";
                                break;
                        }
                    }
                    else if (_signal is AnalogSignal)
                    {
                        var thisSignal = _signal as AnalogSignal;
                        var range = thisSignal.MaxValue - thisSignal.MinValue;
                        if (sample.Value < minVal)
                        {
                            minVal = sample.Value;
                            minValString = minVal.FormatDecimal(thisSignal.Precision > -1 ? thisSignal.Precision : 4);
                        }
                        else if (sample.Value > maxVal)
                        {
                            maxVal = sample.Value;
                            maxValString = maxVal.FormatDecimal(thisSignal.Precision > -1 ? thisSignal.Precision : 4);
                        }

                        if (isFirstSample)
                        {
                            minVal = thisSignal.State;
                            maxVal = thisSignal.State;
                            minValString = minVal.FormatDecimal(thisSignal.Precision > -1 ? thisSignal.Precision : 4);
                            maxValString = maxVal.FormatDecimal(thisSignal.Precision > -1 ? thisSignal.Precision : 4);
                        }

                        y2 = height - (int)(System.Math.Abs(sample.Value - thisSignal.MinValue) / range * height);
                        if (y2 < 0) y2 = 0;
                        if (y2 > height) y2 = height;

                        correlatedY2 = height - (int)(System.Math.Abs(sample.CorrelatedValue - thisSignal.MinValue) /
                                                       range * height);
                        if (correlatedY2 < 0) correlatedY2 = 0;
                        if (correlatedY2 > height) correlatedY2 = height;


                        zeroHeight = height - (int)(System.Math.Abs(-thisSignal.MinValue) / range * height);
                        if (zeroHeight < 0) zeroHeight = 0;
                        if (zeroHeight > height) zeroHeight = height;

                        value = sample.Value.FormatDecimal(thisSignal.Precision > -1 ? thisSignal.Precision : 4);
                        rangeMinValue =
                            thisSignal.MinValue.FormatDecimal(thisSignal.Precision > -1 ? thisSignal.Precision : 4);
                        rangeMaxValue =
                            thisSignal.MaxValue.FormatDecimal(thisSignal.Precision > -1 ? thisSignal.Precision : 4);
                    }
                    if (isFirstSample)
                    {
                        x1 = x2;
                        y1 = y2;
                        correlatedY1 = y2;

                        minY2 = y2;
                        maxY2 = y2;
                        isFirstSample = false;
                    }
                    if (y2 < minY2)
                    {
                        minY2 = y2;
                    }
                    else if (y2 > maxY2)
                    {
                        maxY2 = y2;
                    }

                    valueCurvePointList.Add(new PointF(x1, y1));
                    valueCurvePointList.Add(new PointF(x2, y2));

                    correlatedValueCurvePointList.Add(new PointF(x1, correlatedY1));
                    correlatedValueCurvePointList.Add(new PointF(x2, correlatedY2));


                    graphics.FillPolygon(AreaUnderTheCurveBrush,
                        new[] { new PointF(x1, y1), new PointF(x2, y2), new PointF(x2, height), new PointF(x1, height) });
                    x1 = x2;
                    y1 = y2;
                    correlatedY1 = correlatedY2;
                }
                if (_signal is AnalogSignal && (_signal as AnalogSignal).IsVoltage)
                {
                    minValString += " V";
                    maxValString += " V";
                    rangeMinValue += " V";
                    rangeMaxValue += " V";
                    value += " V";
                }
                for (var x = width + xOffset; x >= 0; x -= width / numXSegments)
                    graphics.DrawLine(GridLinePen, new PointF(x, 0), new PointF(x, height));
                for (var y = 0.0f; y <= height; y += height / numYSegments)
                    graphics.DrawLine(GridLinePen, new PointF(0, y), new PointF(width, y));
                graphics.DrawLine(MinValueLinePen, new PointF(0, minY2), new PointF(width, minY2));
                graphics.DrawLine(MaxValueLinePen, new PointF(0, maxY2), new PointF(width, maxY2));

                if ((_signal as AnalogSignal)?.TimeConstant != null)
                {
                    if (correlatedValueCurvePointList != null && correlatedValueCurvePointList.Count > 0)
                    {
                        graphics.DrawLines(CorrelatedValueCurvePen, correlatedValueCurvePointList.ToArray());
                    }
                }

                if (valueCurvePointList != null && valueCurvePointList.Count > 0)
                {
                    graphics.DrawLines(ValueCurvePen, valueCurvePointList.ToArray());
                }
                graphics.DrawLine(ValueLinePen, new PointF(0, y2), new PointF(width, y2));


                graphics.DrawLine(ZeroLinePen, new PointF(0, zeroHeight), new PointF(width, zeroHeight));

                var valueTextSize = graphics.MeasureString(value, ValueFont);
                var valueFormat = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Center
                };
                var valueRectangle = new RectangleF(0, System.Math.Max(y2 - valueTextSize.Height, 0), width,
                    valueTextSize.Height);
                graphics.DrawString(value, ValueFont, ValueBrush, valueRectangle, valueFormat);

                var minValueTextSize = graphics.MeasureString(minValString, MinValueFont);
                var minValueFormat = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center
                };
                var minValueRectangle = new RectangleF(0, System.Math.Min(maxY2 + 2, height), width,
                    minValueTextSize.Height);
                graphics.DrawString($"Min:{minValString}", MinValueFont, MinValueBrush, minValueRectangle,
                    minValueFormat);

                var maxValueTextSize = graphics.MeasureString(maxValString, MaxValueFont);
                var maxValueFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                var maxValueRectangle = new RectangleF(0, System.Math.Max(minY2 - maxValueTextSize.Height, 0), width,
                    maxValueTextSize.Height);
                graphics.DrawString($"Max:{maxValString}", MaxValueFont, MaxValueBrush, maxValueRectangle,
                    maxValueFormat);
            }

            graphics.Clip = originalClip;
            graphics.Transform = originalTransform;
            graphics.DrawString(_signal.SubcollectionName, SubcollectionNameFont, SubcollectionNameBrush,
                new RectangleF(0, 0, width, topMarginHeight));
            graphics.DrawString(_signal.FriendlyName, FriendlyNameFont, FriendlyNameBrush,
                new RectangleF(0, topMarginHeight / 2, width, topMarginHeight));
            graphics.DrawString(rangeMaxValue, ScaleFont, ScaleFontBrush, new RectangleF(0, 0, width, topMarginHeight),
                new StringFormat {Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far});
            graphics.DrawString(rangeMinValue, ScaleFont, ScaleFontBrush,
                new RectangleF(0, targetRectangle.Height - bottomMarginHeight, width, bottomMarginHeight),
                new StringFormat {Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far});

            if (_signal is TextSignal)
            {
                var stringToDisplay = (_signal as TextSignal).State;
                var stringFormat = new StringFormat();
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.Alignment = StringAlignment.Center;
                graphics.DrawString(stringToDisplay, XBigFont, ValueBrush, targetRectangle, stringFormat);
            }
        }

        private void AnalogSignalChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            CaptureNewSample();
        }

        private void CaptureNewSample()
        {
            lock (_signalStateHistoryLock)
            {
                PurgeOldSamples();
                var signal = _signal as AnalogSignal;
                if (signal != null)
                {
                    _signalStateHistory.Add(new TimestampedDecimal
                    {
                        Timestamp = DateTime.UtcNow,
                        Value = signal.State,
                        CorrelatedValue = signal.CorrelatedState
                    });
                }
                else if (_signal is DigitalSignal)
                {
                    _signalStateHistory.Add(new TimestampedDecimal
                    {
                        Timestamp = DateTime.UtcNow,
                        Value = ((DigitalSignal) _signal).State ? 1 : 0
                    });
                }
            }
        }

        private void DigitalSignalChanged(object sender, DigitalSignalChangedEventArgs args)
        {
            CaptureNewSample();
        }

        private void PurgeOldSamples()
        {
            lock (_signalStateHistoryLock)
            {
                _signalStateHistory.RemoveAll(x => x.Timestamp < DateTime.UtcNow.Subtract(_duration));
            }
        }

        private void RegisterForChangedEvent(Signal signal)
        {
            var analogSignal = signal as AnalogSignal;
            if (analogSignal != null)
            {
                analogSignal.SignalChanged += AnalogSignalChanged;
            }
            else if (signal is DigitalSignal)
            {
                ((DigitalSignal) signal).SignalChanged += DigitalSignalChanged;
            }
        }
    }
}