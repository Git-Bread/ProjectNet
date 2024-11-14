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
            var action = result.Prediction;
            return action;
        }
        public static float CombatDetermine(string input)
        {
            //Load sample data
            var sampleData = new CombatDeterminer.ModelInput()
            {
                Col0 = input,
            };

            //Load model and predict output
            var result = CombatDeterminer.Predict(sampleData);
            var move = result.Prediction;
            return move;

        }
    }
}