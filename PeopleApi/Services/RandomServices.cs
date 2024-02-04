namespace PeopleApi.Services
{
    public class RandomServices : IRandomService
    {
        private readonly int _value;
        public int Value
        {
            get => _value;
        }

        public RandomServices()
        {
            _value = new Random().Next(0, 1000);
        }
    }
}
