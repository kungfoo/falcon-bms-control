using System;
using System.Drawing;
using System.Windows.Forms;


namespace Common.UI.UserControls
{
    /// <summary>
    /// Represents the "dot" in an IP Address composite user control
    /// </summary>
   public class DotControl : Control
   {
      #region Public Properties

      public override Size MinimumSize
      {
         get
         {
             Size minimumSize = Size.Empty;
             using (Graphics g = Graphics.FromHwnd(Handle))
             {

                 minimumSize = TextRenderer.MeasureText(g,
                    Text, Font, Size,
                    _textFormatFlags);
             }
            return minimumSize;
         }
      }

      public bool ReadOnly
      {
         get
         {
            return _readOnly;
         }
         set
         {
            _readOnly = value;
            Invalidate();
         }
      }

      #endregion // Public Properties

      #region Public Methods

      public override string ToString()
      {
         return Text;
      }

      #endregion // Public Methods

      #region Constructors

      public DotControl()
      {
         Text = ".";

         BackColor = SystemColors.Window;
         Size = MinimumSize;
         TabStop = false;

         SetStyle( ControlStyles.AllPaintingInWmPaint, true );
         SetStyle( ControlStyles.OptimizedDoubleBuffer, true );
         SetStyle( ControlStyles.ResizeRedraw, true );
         SetStyle( ControlStyles.UserPaint, true );
      }

      #endregion // Constructors

      #region Protected Methods

      protected override void OnFontChanged( EventArgs e )
      {
         base.OnFontChanged( e );
         Size = MinimumSize;
      }

      protected override void OnPaint( PaintEventArgs e )
      {
         base.OnPaint( e );

         Color backColor = BackColor;

         if ( !_backColorChanged )
         {
            if ( !Enabled || ReadOnly )
            {
               backColor = SystemColors.Control;
            }
         }

         Color textColor = ForeColor;

         if ( !Enabled )
         {
            textColor = SystemColors.GrayText;
         }
         else if ( ReadOnly )
         {
            if ( !_backColorChanged )
            {
               textColor = SystemColors.WindowText;
            }
         }

         using ( SolidBrush backgroundBrush = new SolidBrush( backColor ) )
         {
            e.Graphics.FillRectangle( backgroundBrush, ClientRectangle );
         }

         TextRenderer.DrawText( e.Graphics, Text, Font, ClientRectangle,
            textColor, _textFormatFlags );
      }

      protected override void OnParentBackColorChanged( EventArgs e )
      {
         base.OnParentBackColorChanged( e );
         BackColor = Parent.BackColor;
         _backColorChanged = true;
      }

      protected override void OnParentForeColorChanged( EventArgs e )
      {
         base.OnParentForeColorChanged( e );
         ForeColor = Parent.ForeColor;
      }

      protected override void OnSizeChanged( EventArgs e )
      {
         base.OnSizeChanged( e );
         base.Size = MinimumSize;
      }

      #endregion // Protected Methods

      #region Private Data

      private bool _backColorChanged;
      private bool _readOnly;

      private TextFormatFlags _textFormatFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.NoPrefix |
         TextFormatFlags.SingleLine | TextFormatFlags.NoPadding;

      #endregion // Private Data
   }
}
