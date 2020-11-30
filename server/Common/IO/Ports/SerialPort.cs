using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Text;

namespace Common.IO.Ports
{
    // Summary:
    //     Represents a serial port resource.
    [MonitoringDescription("SerialPortDesc")]
    public class SerialPort : Component, ISerialPort, IDisposable
    {
        // Summary:
        //     Indicates that no time-out should occur.
        public const int InfiniteTimeout = -1;

        private readonly System.IO.Ports.SerialPort _serialPort;

        // Summary:
        //     Initializes a new instance of the System.IO.Ports.SerialPort class.
        public SerialPort() : this(new System.IO.Ports.SerialPort())
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.Ports.SerialPort class using
        //     the specified System.ComponentModel.IContainer object.
        //
        // Parameters:
        //   container:
        //     An interface to a container.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The specified port could not be found or opened.
        public SerialPort(IContainer container) : this(new System.IO.Ports.SerialPort(container))
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.Ports.SerialPort class using
        //     the specified port name.
        //
        // Parameters:
        //   portName:
        //     The port to use (for example, COM1).
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The specified port could not be found or opened.
        public SerialPort(string portName) : this(new System.IO.Ports.SerialPort(portName))
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.Ports.SerialPort class using
        //     the specified port name and baud rate.
        //
        // Parameters:
        //   portName:
        //     The port to use (for example, COM1).
        //
        //   baudRate:
        //     The baud rate.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The specified port could not be found or opened.
        public SerialPort(string portName, int baudRate) : this(new System.IO.Ports.SerialPort(portName, baudRate))
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.Ports.SerialPort class using
        //     the specified port name, baud rate, and parity bit.
        //
        // Parameters:
        //   portName:
        //     The port to use (for example, COM1).
        //
        //   baudRate:
        //     The baud rate.
        //
        //   parity:
        //     One of the System.IO.Ports.SerialPort.Parity values.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The specified port could not be found or opened.
        public SerialPort(string portName, int baudRate, Parity parity) : this(
            new System.IO.Ports.SerialPort(portName, baudRate, parity))
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.Ports.SerialPort class using
        //     the specified port name, baud rate, parity bit, and data bits.
        //
        // Parameters:
        //   portName:
        //     The port to use (for example, COM1).
        //
        //   baudRate:
        //     The baud rate.
        //
        //   parity:
        //     One of the System.IO.Ports.SerialPort.Parity values.
        //
        //   dataBits:
        //     The data bits value.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The specified port could not be found or opened.
        public SerialPort(string portName, int baudRate, Parity parity, int dataBits) : this(
            new System.IO.Ports.SerialPort(portName, baudRate, parity, dataBits))
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.Ports.SerialPort class using
        //     the specified port name, baud rate, parity bit, data bits, and stop bit.
        //
        // Parameters:
        //   portName:
        //     The port to use (for example, COM1).
        //
        //   baudRate:
        //     The baud rate.
        //
        //   parity:
        //     One of the System.IO.Ports.SerialPort.Parity values.
        //
        //   dataBits:
        //     The data bits value.
        //
        //   stopBits:
        //     One of the System.IO.Ports.SerialPort.StopBits values.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The specified port could not be found or opened.
        public SerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits) : this(
            new System.IO.Ports.SerialPort(portName, baudRate, parity, dataBits, stopBits))
        {
        }

        private SerialPort(System.IO.Ports.SerialPort serialPort = null)
        {
            _serialPort = serialPort ?? new System.IO.Ports.SerialPort();
        }

        /// <summary>
        ///     Public implementation of IDisposable.Dispose().  Cleans up
        ///     managed and unmanaged resources used by this
        ///     object before allowing garbage collection
        /// </summary>
        public new void Dispose()
        {
            Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        // Summary:
        //     Gets the underlying System.IO.Stream object for a System.IO.Ports.SerialPort
        //     object.
        //
        // Returns:
        //     A System.IO.Stream object.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The stream is closed. This can occur because the System.IO.Ports.SerialPort.Open()
        //     method has not been called or the System.IO.Ports.SerialPort.Close() method
        //     has been called.
        //
        //   System.NotSupportedException:
        //     The stream is in a .NET Compact Framework application and one of the following
        //     methods was called:System.IO.Stream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)System.IO.Stream.EndRead(System.IAsyncResult)System.IO.Stream.EndWrite(System.IAsyncResult)The
        //     .NET Compact Framework does not support the asynchronous model with base
        //     streams.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Stream BaseStream => _serialPort.BaseStream;

        //
        // Summary:
        //     Gets or sets the serial baud rate.
        //
        // Returns:
        //     The baud rate.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     The baud rate specified is less than or equal to zero, or is greater than
        //     the maximum allowable baud rate for the device.
        //
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        [Browsable(true)]
        [DefaultValue(9600)]
        [MonitoringDescription("BaudRate")]
        public int BaudRate
        {
            get => _serialPort.BaudRate;
            set => _serialPort.BaudRate = value;
        }

        //
        // Summary:
        //     Gets or sets the break signal state.
        //
        // Returns:
        //     true if the port is in a break state; otherwise, false.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or -An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        //
        //   System.InvalidOperationException:
        //     The stream is closed. This can occur because the System.IO.Ports.SerialPort.Open()
        //     method has not been called or the System.IO.Ports.SerialPort.Close() method
        //     has been called.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool BreakState
        {
            get => _serialPort.BreakState;
            set => _serialPort.BreakState = value;
        }

        //
        // Summary:
        //     Gets the number of bytes of data in the receive buffer.
        //
        // Returns:
        //     The number of bytes of data in the receive buffer.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The port is not open.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BytesToRead => _serialPort.BytesToRead;

        //
        // Summary:
        //     Gets the number of bytes of data in the send buffer.
        //
        // Returns:
        //     The number of bytes of data in the send buffer.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state.
        //
        //   System.InvalidOperationException:
        //     The stream is closed. This can occur because the System.IO.Ports.SerialPort.Open()
        //     method has not been called or the System.IO.Ports.SerialPort.Close() method
        //     has been called.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BytesToWrite => _serialPort.BytesToWrite;

        //
        // Summary:
        //     Gets the state of the Carrier Detect line for the port.
        //
        // Returns:
        //     true if the carrier is detected; otherwise, false.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        //
        //   System.InvalidOperationException:
        //     The stream is closed. This can occur because the System.IO.Ports.SerialPort.Open()
        //     method has not been called or the System.IO.Ports.SerialPort.Close() method
        //     has been called.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CDHolding => _serialPort.CDHolding;

        // Summary:
        //     Closes the port connection, sets the System.IO.Ports.SerialPort.IsOpen property
        //     to false, and disposes of the internal System.IO.Stream object.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state.- or -An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        public void Close()
        {
            _serialPort.Close();
        }

        //
        // Summary:
        //     Gets the state of the Clear-to-Send line.
        //
        // Returns:
        //     true if the Clear-to-Send line is detected; otherwise, false.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        //
        //   System.InvalidOperationException:
        //     The stream is closed. This can occur because the System.IO.Ports.SerialPort.Open()
        //     method has not been called or the System.IO.Ports.SerialPort.Close() method
        //     has been called.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CtsHolding => _serialPort.CtsHolding;

        //
        // Summary:
        //     Gets or sets the standard length of data bits per byte.
        //
        // Returns:
        //     The data bits length.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or -An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        //
        //   System.ArgumentOutOfRangeException:
        //     The data bits value is less than 5 or more than 8.
        [Browsable(true)]
        [DefaultValue(8)]
        [MonitoringDescription("DataBits")]
        public int DataBits
        {
            get => _serialPort.DataBits;
            set => _serialPort.DataBits = value;
        }

        // Summary:
        //     Represents the method that will handle the data received event of a System.IO.Ports.SerialPort
        //     object.
        [MonitoringDescription("SerialDataReceived")]
        public event SerialDataReceivedEventHandler DataReceived
        {
            add => _serialPort.DataReceived += value;
            remove => _serialPort.DataReceived -= value;
        }

        //
        // Summary:
        //     Discards data from the serial driver's receive buffer.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or -An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        //
        //   System.InvalidOperationException:
        //     The stream is closed. This can occur because the System.IO.Ports.SerialPort.Open()
        //     method has not been called or the System.IO.Ports.SerialPort.Close() method
        //     has been called.
        public void DiscardInBuffer()
        {
            _serialPort.DiscardInBuffer();
        }

        //
        // Summary:
        //     Gets or sets a value indicating whether null bytes are ignored when transmitted
        //     between the port and the receive buffer.
        //
        // Returns:
        //     true if null bytes are ignored; otherwise false. The default is false.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        //
        //   System.InvalidOperationException:
        //     The stream is closed. This can occur because the System.IO.Ports.SerialPort.Open()
        //     method has not been called or the System.IO.Ports.SerialPort.Close() method
        //     has been called.
        [Browsable(true)]
        [DefaultValue(false)]
        [MonitoringDescription("DiscardNull")]
        public bool DiscardNull
        {
            get => _serialPort.DiscardNull;
            set => _serialPort.DiscardNull = value;
        }

        //
        // Summary:
        //     Discards data from the serial driver's transmit buffer.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        //
        //   System.InvalidOperationException:
        //     The stream is closed. This can occur because the System.IO.Ports.SerialPort.Open()
        //     method has not been called or the System.IO.Ports.SerialPort.Close() method
        //     has been called.
        public void DiscardOutBuffer()
        {
            _serialPort.DiscardOutBuffer();
        }

        //
        // Summary:
        //     Gets the state of the Data Set Ready (DSR) signal.
        //
        // Returns:
        //     true if a Data Set Ready signal has been sent to the port; otherwise, false.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        //
        //   System.InvalidOperationException:
        //     The stream is closed. This can occur because the System.IO.Ports.SerialPort.Open()
        //     method has not been called or the System.IO.Ports.SerialPort.Close() method
        //     has been called.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DsrHolding => _serialPort.DsrHolding;

        //
        // Summary:
        //     Gets or sets a value that enables the Data Terminal Ready (DTR) signal during
        //     serial communication.
        //
        // Returns:
        //     true to enable Data Terminal Ready (DTR); otherwise, false. The default is
        //     false.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        [Browsable(true)]
        [DefaultValue(false)]
        [MonitoringDescription("DtrEnable")]
        public bool DtrEnable
        {
            get => _serialPort.DtrEnable;
            set => _serialPort.DtrEnable = value;
        }

        //
        // Summary:
        //     Gets or sets the byte encoding for pre- and post-transmission conversion
        //     of text.
        //
        // Returns:
        //     An System.Text.Encoding object. The default is System.Text.ASCIIEncoding.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The System.IO.Ports.SerialPort.Encoding property was set to null.
        //
        //   System.ArgumentException:
        //     The System.IO.Ports.SerialPort.Encoding property was set to an encoding that
        //     is not System.Text.ASCIIEncoding, System.Text.UTF8Encoding, System.Text.UTF32Encoding,
        //     System.Text.UnicodeEncoding, one of the Windows single byte encodings, or
        //     one of the Windows double byte encodings.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MonitoringDescription("Encoding")]
        public Encoding Encoding
        {
            get => _serialPort.Encoding;
            set => _serialPort.Encoding = value;
        }

        //
        // Summary:
        //     Represents the method that handles the error event of a System.IO.Ports.SerialPort
        //     object.
        [MonitoringDescription("SerialErrorReceived")]
        public event SerialErrorReceivedEventHandler ErrorReceived
        {
            add => _serialPort.ErrorReceived += value;
            remove => _serialPort.ErrorReceived -= value;
        }

        //
        // Summary:
        //     Gets or sets the handshaking protocol for serial port transmission of data.
        //
        // Returns:
        //     One of the System.IO.Ports.Handshake values. The default is None.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        //
        //   System.ArgumentOutOfRangeException:
        //     The value passed is not a valid value in the System.IO.Ports.Handshake enumeration.
        [Browsable(true)]
        [MonitoringDescription("Handshake")]
        public Handshake Handshake
        {
            get => _serialPort.Handshake;
            set => _serialPort.Handshake = value;
        }

        //
        // Summary:
        //     Gets a value indicating the open or closed status of the System.IO.Ports.SerialPort
        //     object.
        //
        // Returns:
        //     true if the serial port is open; otherwise, false. The default is false.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The System.IO.Ports.SerialPort.IsOpen value passed is null.
        //
        //   System.ArgumentException:
        //     The System.IO.Ports.SerialPort.IsOpen value passed is an empty string ("").
        [Browsable(false)]
        public bool IsOpen => _serialPort.IsOpen;

        //
        // Summary:
        //     Gets or sets the value used to interpret the end of a call to the System.IO.Ports.SerialPort.ReadLine()
        //     and System.IO.Ports.SerialPort.WriteLine(System.String) methods.
        //
        // Returns:
        //     A value that represents the end of a line. The default is a line feed, (System.Environment.NewLine).
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The property value is empty.
        //
        //   System.ArgumentNullException:
        //     The property value is null.
        [Browsable(false)]
        [DefaultValue(@"
")]
        [MonitoringDescription("NewLine")]
        public string NewLine
        {
            get => _serialPort.NewLine;
            set => _serialPort.NewLine = value;
        }

        //
        // Summary:
        //     Opens a new serial port connection.
        //
        // Exceptions:
        //   System.UnauthorizedAccessException:
        //     Access is denied to the port.- or -The current process, or another process
        //     on the system, already has the specified COM port open either by a System.IO.Ports.SerialPort
        //     instance or in unmanaged code.
        //
        //   System.ArgumentOutOfRangeException:
        //     One or more of the properties for this instance are invalid. For example,
        //     the System.IO.Ports.SerialPort.Parity, System.IO.Ports.SerialPort.DataBits,
        //     or System.IO.Ports.SerialPort.Handshake properties are not valid values;
        //     the System.IO.Ports.SerialPort.BaudRate is less than or equal to zero; the
        //     System.IO.Ports.SerialPort.ReadTimeout or System.IO.Ports.SerialPort.WriteTimeout
        //     property is less than zero and is not System.IO.Ports.SerialPort.InfiniteTimeout.
        //
        //   System.ArgumentException:
        //     The port name does not begin with "COM". - or -The file type of the port
        //     is not supported.
        //
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        //
        //   System.InvalidOperationException:
        //     The specified port on the current instance of the System.IO.Ports.SerialPort
        //     is already open.
        public void Open()
        {
            _serialPort.Open();
        }

        //
        // Summary:
        //     Gets or sets the parity-checking protocol.
        //
        // Returns:
        //     One of the enumeration values that represents the parity-checking protocol.
        //     The default is System.IO.Ports.Parity.None.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        //
        //   System.ArgumentOutOfRangeException:
        //     The System.IO.Ports.SerialPort.Parity value passed is not a valid value in
        //     the System.IO.Ports.Parity enumeration.
        [Browsable(true)]
        [MonitoringDescription("Parity")]
        public Parity Parity
        {
            get => _serialPort.Parity;
            set => _serialPort.Parity = value;
        }

        //
        // Summary:
        //     Gets or sets the byte that replaces invalid bytes in a data stream when a
        //     parity error occurs.
        //
        // Returns:
        //     A byte that replaces invalid bytes.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        [Browsable(true)]
        [DefaultValue(63)]
        [MonitoringDescription("ParityReplace")]
        public byte ParityReplace
        {
            get => _serialPort.ParityReplace;
            set => _serialPort.ParityReplace = value;
        }

        //
        // Summary:
        //     Represents the method that will handle the serial pin changed event of a
        //     System.IO.Ports.SerialPort object.
        [MonitoringDescription("SerialPinChanged")]
        public event SerialPinChangedEventHandler PinChanged
        {
            add => _serialPort.PinChanged += value;
            remove => _serialPort.PinChanged -= value;
        }

        //
        // Summary:
        //     Gets or sets the port for communications, including but not limited to all
        //     available COM ports.
        //
        // Returns:
        //     The communications port. The default is COM1.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The System.IO.Ports.SerialPort.PortName property was set to a value with
        //     a length of zero.-or-The System.IO.Ports.SerialPort.PortName property was
        //     set to a value that starts with "\\".-or-The port name was not valid.
        //
        //   System.ArgumentNullException:
        //     The System.IO.Ports.SerialPort.PortName property was set to null.
        //
        //   System.InvalidOperationException:
        //     The specified port is open.
        [Browsable(true)]
        [DefaultValue("COM1")]
        [MonitoringDescription("PortName")]
        public string PortName
        {
            get => _serialPort.PortName;
            set => _serialPort.PortName = value;
        }

        //
        // Summary:
        //     Reads a number of bytes from the System.IO.Ports.SerialPort input buffer
        //     and writes those bytes into a byte array at the specified offset.
        //
        // Parameters:
        //   buffer:
        //     The byte array to write the input to.
        //
        //   offset:
        //     The offset in the buffer array to begin writing.
        //
        //   count:
        //     The number of bytes to read.
        //
        // Returns:
        //     The number of bytes read.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The buffer passed is null.
        //
        //   System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   System.ArgumentOutOfRangeException:
        //     The offset or count parameters are outside a valid region of the buffer being
        //     passed. Either offset or count is less than zero.
        //
        //   System.ArgumentException:
        //     offset plus count is greater than the length of the buffer.
        //
        //   System.TimeoutException:
        //     No bytes were available to read.
        public int Read(byte[] buffer, int offset, int count)
        {
            return _serialPort.Read(buffer, offset, count);
        }

        //
        // Summary:
        //     Reads a number of characters from the System.IO.Ports.SerialPort input buffer
        //     and writes them into an array of characters at a given offset.
        //
        // Parameters:
        //   buffer:
        //     The character array to write the input to.
        //
        //   offset:
        //     The offset in the buffer array to begin writing.
        //
        //   count:
        //     The number of characters to read.
        //
        // Returns:
        //     The number of characters read.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     offset plus count is greater than the length of the buffer.- or -count is
        //     1 and there is a surrogate character in the buffer.
        //
        //   System.ArgumentNullException:
        //     The buffer passed is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     The offset or count parameters are outside a valid region of the buffer being
        //     passed. Either offset or count is less than zero.
        //
        //   System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   System.TimeoutException:
        //     No characters were available to read.
        public int Read(char[] buffer, int offset, int count)
        {
            return _serialPort.Read(buffer, offset, count);
        }

        //
        // Summary:
        //     Gets or sets the size of the System.IO.Ports.SerialPort input buffer.
        //
        // Returns:
        //     The buffer size, in bytes. The default value is 4096.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     The System.IO.Ports.SerialPort.ReadBufferSize value set is less than or equal
        //     to zero.
        //
        //   System.InvalidOperationException:
        //     The System.IO.Ports.SerialPort.ReadBufferSize property was set while the
        //     stream was open.
        //
        //   System.IO.IOException:
        //     The System.IO.Ports.SerialPort.ReadBufferSize property was set to an odd
        //     integer value.
        [Browsable(true)]
        [DefaultValue(4096)]
        [MonitoringDescription("ReadBufferSize")]
        public int ReadBufferSize
        {
            get => _serialPort.ReadBufferSize;
            set => _serialPort.ReadBufferSize = value;
        }

        //
        // Summary:
        //     Synchronously reads one byte from the System.IO.Ports.SerialPort input buffer.
        //
        // Returns:
        //     The byte, cast to an System.Int32, or -1 if the end of the stream has been
        //     read.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   System.ServiceProcess.TimeoutException:
        //     The operation did not complete before the time-out period ended.- or -No
        //     byte was read.
        public int ReadByte()
        {
            return _serialPort.ReadByte();
        }

        //
        // Summary:
        //     Synchronously reads one character from the System.IO.Ports.SerialPort input
        //     buffer.
        //
        // Returns:
        //     The character that was read.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   System.ServiceProcess.TimeoutException:
        //     The operation did not complete before the time-out period ended.- or -No
        //     character was available in the allotted time-out period.
        public int ReadChar()
        {
            return _serialPort.ReadChar();
        }

        //
        // Summary:
        //     Reads all immediately available bytes, based on the encoding, in both the
        //     stream and the input buffer of the System.IO.Ports.SerialPort object.
        //
        // Returns:
        //     The contents of the stream and the input buffer of the System.IO.Ports.SerialPort
        //     object.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The specified port is not open.
        public string ReadExisting()
        {
            return _serialPort.ReadExisting();
        }

        //
        // Summary:
        //     Reads up to the System.IO.Ports.SerialPort.NewLine value in the input buffer.
        //
        // Returns:
        //     The contents of the input buffer up to the first occurrence of a System.IO.Ports.SerialPort.NewLine
        //     value.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   System.TimeoutException:
        //     The operation did not complete before the time-out period ended.- or -No
        //     bytes were read.
        public string ReadLine()
        {
            return _serialPort.ReadLine();
        }

        //
        // Summary:
        //     Gets or sets the number of milliseconds before a time-out occurs when a read
        //     operation does not finish.
        //
        // Returns:
        //     The number of milliseconds before a time-out occurs when a read operation
        //     does not finish.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        //
        //   System.ArgumentOutOfRangeException:
        //     The read time-out value is less than zero and not equal to System.IO.Ports.SerialPort.InfiniteTimeout.
        [Browsable(true)]
        [DefaultValue(-1)]
        [MonitoringDescription("ReadTimeout")]
        public int ReadTimeout
        {
            get => _serialPort.ReadTimeout;
            set => _serialPort.ReadTimeout = value;
        }

        //
        // Summary:
        //     Reads a string up to the specified value in the input buffer.
        //
        // Parameters:
        //   value:
        //     A value that indicates where the read operation stops.
        //
        // Returns:
        //     The contents of the input buffer up to the specified value.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The length of the value parameter is 0.
        //
        //   System.ArgumentNullException:
        //     The value parameter is null.
        //
        //   System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   System.TimeoutException:
        //     The operation did not complete before the time-out period ended.
        public string ReadTo(string value)
        {
            return _serialPort.ReadTo(value);
        }

        //
        // Summary:
        //     Gets or sets the number of bytes in the internal input buffer before a System.IO.Ports.SerialPort.DataReceived
        //     event occurs.
        //
        // Returns:
        //     The number of bytes in the internal input buffer before a System.IO.Ports.SerialPort.DataReceived
        //     event is fired. The default is 1.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     The System.IO.Ports.SerialPort.ReceivedBytesThreshold value is less than
        //     or equal to zero.
        [Browsable(true)]
        [DefaultValue(1)]
        [MonitoringDescription("ReceivedBytesThreshold")]
        public int ReceivedBytesThreshold
        {
            get => _serialPort.ReceivedBytesThreshold;
            set => _serialPort.ReceivedBytesThreshold = value;
        }

        //
        // Summary:
        //     Gets or sets a value indicating whether the Request to Send (RTS) signal
        //     is enabled during serial communication.
        //
        // Returns:
        //     true to enable Request to Transmit (RTS); otherwise, false. The default is
        //     false.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The value of the System.IO.Ports.SerialPort.RtsEnable property was set or
        //     retrieved while the System.IO.Ports.SerialPort.Handshake property is set
        //     to the System.IO.Ports.Handshake.RequestToSend value or the System.IO.Ports.Handshake.RequestToSendXOnXOff
        //     value.
        //
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        [Browsable(true)]
        [DefaultValue(false)]
        [MonitoringDescription("RtsEnable")]
        public bool RtsEnable
        {
            get => _serialPort.RtsEnable;
            set => _serialPort.RtsEnable = value;
        }

        //
        // Summary:
        //     Gets or sets the standard number of stopbits per byte.
        //
        // Returns:
        //     One of the System.IO.Ports.StopBits values.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     The System.IO.Ports.SerialPort.StopBits value is System.IO.Ports.StopBits.None.
        //
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        [Browsable(true)]
        [MonitoringDescription("StopBits")]
        public StopBits StopBits
        {
            get => _serialPort.StopBits;
            set => _serialPort.StopBits = value;
        }

        //
        // Summary:
        //     Writes the specified string to the serial port.
        //
        // Parameters:
        //   text:
        //     The string for output.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   System.ArgumentNullException:
        //     str is null.
        //
        //   System.ServiceProcess.TimeoutException:
        //     The operation did not complete before the time-out period ended.
        public void Write(string text)
        {
            _serialPort.Write(text);
        }

        //
        // Summary:
        //     Writes a specified number of bytes to the serial port using data from a buffer.
        //
        // Parameters:
        //   buffer:
        //     The byte array that contains the data to write to the port.
        //
        //   offset:
        //     The zero-based byte offset in the buffer parameter at which to begin copying
        //     bytes to the port.
        //
        //   count:
        //     The number of bytes to write.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The buffer passed is null.
        //
        //   System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   System.ArgumentOutOfRangeException:
        //     The offset or count parameters are outside a valid region of the buffer being
        //     passed. Either offset or count is less than zero.
        //
        //   System.ArgumentException:
        //     offset plus count is greater than the length of the buffer.
        //
        //   System.ServiceProcess.TimeoutException:
        //     The operation did not complete before the time-out period ended.
        public void Write(byte[] buffer, int offset, int count)
        {
            _serialPort.Write(buffer, offset, count);
        }

        //
        // Summary:
        //     Writes a specified number of characters to the serial port using data from
        //     a buffer.
        //
        // Parameters:
        //   buffer:
        //     The character array that contains the data to write to the port.
        //
        //   offset:
        //     The zero-based byte offset in the buffer parameter at which to begin copying
        //     bytes to the port.
        //
        //   count:
        //     The number of characters to write.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The buffer passed is null.
        //
        //   System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   System.ArgumentOutOfRangeException:
        //     The offset or count parameters are outside a valid region of the buffer being
        //     passed. Either offset or count is less than zero.
        //
        //   System.ArgumentException:
        //     offset plus count is greater than the length of the buffer.
        //
        //   System.ServiceProcess.TimeoutException:
        //     The operation did not complete before the time-out period ended.
        public void Write(char[] buffer, int offset, int count)
        {
            _serialPort.Write(buffer, offset, count);
        }

        //
        // Summary:
        //     Gets or sets the size of the serial port output buffer.
        //
        // Returns:
        //     The size of the output buffer. The default is 2048.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     The System.IO.Ports.SerialPort.WriteBufferSize value is less than or equal
        //     to zero.
        //
        //   System.InvalidOperationException:
        //     The System.IO.Ports.SerialPort.WriteBufferSize property was set while the
        //     stream was open.
        //
        //   System.IO.IOException:
        //     The System.IO.Ports.SerialPort.WriteBufferSize property was set to an odd
        //     integer value.
        [Browsable(true)]
        [DefaultValue(2048)]
        [MonitoringDescription("WriteBufferSize")]
        public int WriteBufferSize
        {
            get => _serialPort.WriteBufferSize;
            set => _serialPort.WriteBufferSize = value;
        }

        //
        // Summary:
        //     Writes the specified string and the System.IO.Ports.SerialPort.NewLine value
        //     to the output buffer.
        //
        // Parameters:
        //   text:
        //     The string to write to the output buffer.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The str parameter is null.
        //
        //   System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   System.TimeoutException:
        //     The System.IO.Ports.SerialPort.WriteLine(System.String) method could not
        //     write to the stream.
        public void WriteLine(string text)
        {
            _serialPort.WriteLine(text);
        }

        //
        // Summary:
        //     Gets or sets the number of milliseconds before a time-out occurs when a write
        //     operation does not finish.
        //
        // Returns:
        //     The number of milliseconds before a time-out occurs. The default is System.IO.Ports.SerialPort.InfiniteTimeout.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or - An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        //
        //   System.ArgumentOutOfRangeException:
        //     The System.IO.Ports.SerialPort.WriteTimeout value is less than zero and not
        //     equal to System.IO.Ports.SerialPort.InfiniteTimeout.
        [Browsable(true)]
        [DefaultValue(-1)]
        [MonitoringDescription("WriteTimeout")]
        public int WriteTimeout
        {
            get => _serialPort.WriteTimeout;
            set => _serialPort.WriteTimeout = value;
        }

        /// <summary>
        ///     Standard finalizer, which will call Dispose() if this object
        ///     is not manually disposed.  Ordinarily called only
        ///     by the garbage collector.
        /// </summary>
        ~SerialPort()
        {
            Dispose();
        }


        //
        // Summary:
        //     Gets an array of serial port names for the current computer.
        //
        // Returns:
        //     An array of serial port names for the current computer.
        //
        // Exceptions:
        //   System.ComponentModel.Win32Exception:
        //     The serial port names could not be queried.
        public static string[] GetPortNames()
        {
            return System.IO.Ports.SerialPort.GetPortNames();
        }

        //
        // Summary:
        //     Releases the unmanaged resources used by the System.IO.Ports.SerialPort and
        //     optionally releases the managed resources.
        //
        // Parameters:
        //   disposing:
        //     true to release both managed and unmanaged resources; false to release only
        //     unmanaged resources.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     The port is in an invalid state. - or -An attempt to set the state of the
        //     underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        protected override void Dispose(bool disposing)
        {
            Util.DisposeObject(_serialPort);
            base.Dispose(disposing);
        }
    }
}