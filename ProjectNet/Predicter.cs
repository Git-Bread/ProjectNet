namespace ProjectNet
{
    public class Predicter
    {
        public static float Determine(string input)
        {
            //Load sample data
            var sampleData = new ActionDeterminer.ModelInput()
            {
                Col0 = input,
            };

            //Load model and predict output
            var result = ActionDeterminer.Predict(sampleData);
            var sentiment = result.Prediction;
            return sentiment;
        }
    }
}