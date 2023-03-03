
using System.Net.Mime;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace STT_example
{
    class Program
    {
        async static Task Speak(SpeechConfig speechConfig, string text)
        {
            // Configure speech synthesis
            speechConfig.SpeechSynthesisLanguage = "zh-TW";
            speechConfig.SpeechSynthesisVoiceName = "zh-TW-HsiaoChenNeural"; //女生 
            speechConfig.SpeechSynthesisVoiceName = "zh-TW-YunJheNeural"; //男生 
            using SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer(speechConfig);

            // Synthesize spoken output
            SpeechSynthesisResult speak = await speechSynthesizer.SpeakTextAsync(text);
            if (speak.Reason != ResultReason.SynthesizingAudioCompleted)
            {
                Console.WriteLine(speak.Reason);
            }
            // Print the response
            Console.WriteLine("\n說了:" + text);
        }


        async static Task FromMic(SpeechConfig speechConfig)
        {
            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var recognizer = new SpeechRecognizer(speechConfig, "zh-tw", audioConfig);
            await Speak(speechConfig, "請透過語音下達指令..., 直到說 '我要離開'");
            Console.WriteLine("嗨~ 請透過語音下達指令..., 直到說 '我要離開' ");
            var text = "";
            while (!text.Contains("離開"))
            {
                var result = await recognizer.RecognizeOnceAsync();
                text = result.Text;
                Console.WriteLine($"\n識別語音 = '{text}' 。\n(如果要結束，請說 '我要離開')");
            }
            await Speak(speechConfig, "喔，你要離開了? bye~bye~");
            Console.WriteLine("\n\n 掰掰~ \n\n");
        }

        static void Main(string[] args)
        {
            // Set console encoding to unicode
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            var speechConfig = SpeechConfig.FromSubscription(
                "___your___key___", "___localtion____");
            FromMic(speechConfig).GetAwaiter().GetResult();
        }
    }
}
