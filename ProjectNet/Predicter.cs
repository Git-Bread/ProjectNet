namespace ProjectNet
{
    public class Predicter
    {
        public static void Determine(string input)
        {
            //Load sample data
            var sampleData = new InputDeterminer.ModelInput()
            {
                Col0 = input,
            };

            //Load model and predict output
            var result = InputDeterminer.Predict(sampleData);
            var sentiment = result.Prediction;
            Console.WriteLine($"Text: {sampleData.Col0}\nSentiment: {sentiment}");
        }
    }
}