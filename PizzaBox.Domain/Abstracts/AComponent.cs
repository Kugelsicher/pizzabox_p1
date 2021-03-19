namespace PizzaBox.Domain.Abstracts
{
    public abstract class AComponent
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value.ToLower();
            }
        }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}