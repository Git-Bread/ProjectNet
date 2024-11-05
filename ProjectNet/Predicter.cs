namespace ProjectNet
{
    public class Predicter
    {
        public void setup()
        {
            //Load sample data
            var sampleData = new InputDeterminer.ModelInput()
            {
                Col0 = @"You open the chest wide.",
            };

            //Load model and predict output
            var result = InputDeterminer.Predict(sampleData);
        }
    }
}