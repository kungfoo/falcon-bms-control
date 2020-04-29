using System;
using System.ComponentModel;
using System.Threading;

namespace Common.SimSupport
{
    public class MorseCode : IDisposable
    {
        private int _charactersPerMinute; //CPM
        private bool _isDisposed;
        private bool _keepSending;
        private int _unitTimeMillis; //standard Morse time unit, in milliseconds
        private BackgroundWorker _worker;

        public MorseCode()
        {
            CharactersPerMinute = 120;
        }

        public int CharactersPerMinute
        {
            get => _charactersPerMinute;
            set
            {
                _charactersPerMinute = value;
                _unitTimeMillis = 6000 / value;
            }
        }

        public string PlainText { get; set; }

        public bool Sending { get; private set; }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                Dispose(true);
            }
            GC.SuppressFinalize(this);
        }

        ~MorseCode()
        {
            Dispose();
        }

        public event EventHandler<UnitTimeTickEventArgs> UnitTimeTick;

        public void StartSending()
        {
            if (Sending) throw new InvalidOperationException("Already sending");
            _worker = new BackgroundWorker();
            _worker.DoWork += WorkerDoWork;
            _worker.RunWorkerAsync();
        }

        public void StopSending()
        {
            _keepSending = false;
            while (Sending)
                Thread.Sleep(20);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopSending();
            }
            _isDisposed = true;
        }

        private static string GetMorsePatternStringForPlainChar(char someChar)
        {
            var toReturn = "";
            switch (char.ToUpper(someChar))
            {
                case 'A':
                    toReturn = ".-";
                    break;
                case 'B':
                    toReturn = "-...";
                    break;
                case 'C':
                    toReturn = "-.-.";
                    break;
                case 'D':
                    toReturn = "-..";
                    break;
                case 'E':
                    toReturn = ".";
                    break;
                case 'F':
                    toReturn = "..-.";
                    break;
                case 'G':
                    toReturn = "--.";
                    break;
                case 'H':
                    toReturn = "....";
                    break;
                case 'I':
                    toReturn = "..";
                    break;
                case 'J':
                    toReturn = ".---";
                    break;
                case 'K':
                    toReturn = "-.-";
                    break;
                case 'L':
                    toReturn = ".-..";
                    break;
                case 'M':
                    toReturn = "--";
                    break;
                case 'N':
                    toReturn = "-.";
                    break;
                case 'O':
                    toReturn = "---";
                    break;
                case 'P':
                    toReturn = ".--.";
                    break;
                case 'Q':
                    toReturn = "--.-";
                    break;
                case 'R':
                    toReturn = ".-.";
                    break;
                case 'S':
                    toReturn = "...";
                    break;
                case 'T':
                    toReturn = "-";
                    break;
                case 'U':
                    toReturn = "..-";
                    break;
                case 'V':
                    toReturn = "...-";
                    break;
                case 'W':
                    toReturn = ".--";
                    break;
                case 'X':
                    toReturn = "-..-";
                    break;
                case 'Y':
                    toReturn = "-.--";
                    break;
                case 'Z':
                    toReturn = "--..";
                    break;
                case '0':
                    toReturn = "-----";
                    break;
                case '1':
                    toReturn = ".----";
                    break;
                case '2':
                    toReturn = "..---";
                    break;
                case '3':
                    toReturn = "...--";
                    break;
                case '4':
                    toReturn = "....-";
                    break;
                case '5':
                    toReturn = ".....";
                    break;
                case '6':
                    toReturn = "-....";
                    break;
                case '7':
                    toReturn = "--...";
                    break;
                case '8':
                    toReturn = "---..";
                    break;
                case '9':
                    toReturn = "----.";
                    break;
                case '.':
                    toReturn = ".-.-.-";
                    break;
                case ',':
                    toReturn = "--..--";
                    break;
                case '?':
                    toReturn = "..--..";
                    break;
                case '\'':
                    toReturn = ".----.";
                    break;
                case '!':
                    toReturn = "-.-.--";
                    break;
                case '/':
                    toReturn = "-..-.";
                    break;
                case '(':
                    toReturn = "-.--.";
                    break;
                case ')':
                    toReturn = "-.--.-";
                    break;
                case '&':
                    toReturn = ".-...";
                    break;
                case ':':
                    toReturn = "---...";
                    break;
                case ';':
                    toReturn = "-.-.-.";
                    break;
                case '+':
                    toReturn = ".-.-.";
                    break;
                case '-':
                    toReturn = "-....-";
                    break;
                case '_':
                    toReturn = "..--.-";
                    break;
                case '"':
                    toReturn = ".-..-.";
                    break;
                case '$':
                    toReturn = "...-..-";
                    break;
                case '@':
                    toReturn = ".--.-.";
                    break;
            }
            return toReturn;
        }

        private static string GetQuinaryStringForMorsePatternChar(char patternChar)
        {
            var toReturn = "";
            switch (patternChar)
            {
                case '.': //dot
                    toReturn += "1";
                    break;
                case '-': //dash
                    toReturn += "111";
                    break;
                case ' ': //intracharacter gap
                    toReturn += "0";
                    break;
                case '_': //short gap (between characters in a word)
                    toReturn += "000";
                    break;
                case '>': //medium gap (between words)
                    toReturn += "0000000";
                    break;
            }
            return toReturn;
        }

        private void OnUnitTimeTick(object sender, UnitTimeTickEventArgs e)
        {
            UnitTimeTick?.Invoke(sender, e);
        }

        private void Send()
        {
            var words = PlainText.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            for (var a = 0; a < words.Length; a++)
            {
                var thisWord = words[a];
                for (var i = 0; i < thisWord.Length; i++)
                {
                    var somePlainChar = thisWord[i];
                    var morsePatternCurrentPlainChar = GetMorsePatternStringForPlainChar(somePlainChar);
                    //get the Morse code pattern for the current character
                    for (var j = 0; j < morsePatternCurrentPlainChar.Length; j++)
                    {
                        var someMorseChar = morsePatternCurrentPlainChar[j];
                        var quinary = GetQuinaryStringForMorsePatternChar(someMorseChar);
                        SendUnits(quinary);
                        if (j < morsePatternCurrentPlainChar.Length - 1)
                        {
                            SendUnits(GetQuinaryStringForMorsePatternChar(' ')); //send intracharacter gap
                        }
                    }
                    if (i < thisWord.Length - 1)
                    {
                        SendUnits(GetQuinaryStringForMorsePatternChar('_'));
                        //send short gap (between characters in a plaintext word
                    }
                }
                if (a < words.Length - 1)
                {
                    SendUnits(GetQuinaryStringForMorsePatternChar('>')); //send word gap
                }
            }
        }

        private void SendUnits(string quinary)
        {
            foreach (var aChar in quinary)
            {
                OnUnitTimeTick(this, new UnitTimeTickEventArgs(aChar == '1'));
                Thread.Sleep(_unitTimeMillis);
            }
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            Sending = true;
            do
            {
                Send();
                if (_keepSending)
                {
                    SendUnits(GetQuinaryStringForMorsePatternChar(' ')); //insert gap before repeating
                }
            } while (_keepSending);
            Sending = false;
        }
    }
}