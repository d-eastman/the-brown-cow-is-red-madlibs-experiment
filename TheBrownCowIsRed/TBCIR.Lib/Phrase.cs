namespace TBCIR.Lib
{
    public abstract class Phrase
    {
        public abstract string GetRandomValue();

        protected string InternalGetRandomValue()
        {
            _LastRandomValue = GetRandomValue();
            return _LastRandomValue;
        }

        private string _LastRandomValue = "";

        protected virtual string LastRandomValue
        {
            get
            {
                return _LastRandomValue;
            }
            set
            {
                _LastRandomValue = value;
            }
        }

        private PartFactory _PartFactory = null;

        protected PartFactory PartFactory
        {
            get
            {
                return _PartFactory;
            }
            set
            {
                _PartFactory = value;
            }
        }
    }
}