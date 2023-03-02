
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace STT_example
{
    class Program
    {
        async static Task FromMic(SpeechConfig speechConfig)
        {
            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var recognizer = new SpeechRecognizer(speechConfig, "zh-tw", audioConfig);

            Console.WriteLine("嗨~ 請透過語音下達指令..., 直到說 '我要離開' ");
            var text = "";
            while (!text.Contains("離開"))
            {
                var result = await recognizer.RecognizeOnceAsync();
                text = result.Text;
                Console.WriteLine($"\n識別語音 = '{text}' 。\n(如果要結束，請說 '我要離開')");
            }
            Console.WriteLine("\n\n 掰掰~ ");
        }

        static void Main(string[] args)
        {
            // Set console encoding to unicode
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            var speechConfig = SpeechConfig.FromSubscription(
                "___your__key____", "___localtion___");
            FromMic(speechConfig).GetAwaiter().GetResult();
        }
    }
}
