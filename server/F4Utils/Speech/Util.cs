namespace F4Utils.Speech
{
    public class Util
    {
        public static void InitWaveFormatEXData(ref WAVEFORMATEX waveFormat)
        {
            waveFormat.wFormatTag = 1;
            waveFormat.nChannels = 1;
            waveFormat.nSamplesPerSec = 8000;
            waveFormat.nAvgBytesPerSec = 16000;
            waveFormat.nBlockAlign = 2;
            waveFormat.wBitsPerSample = 16;
            waveFormat.cbSize = 0;
        }
    }
}