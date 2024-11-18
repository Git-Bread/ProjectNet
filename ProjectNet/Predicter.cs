namespace ProjectNet
{
    //handler for two diffrent models, its two to make the output more accurate due to small sample size of data
    public class Predicter
    {
        //handler for action predction machine model
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
        //handler for combat prediction machine model
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