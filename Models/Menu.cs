using ReactiveUIApplication.Enums;

namespace ReactiveUIApplication.Models
{
    public class Menu
    {
        public MenuOption Option { get; }

        public Menu(MenuOption option)
        {
            Option = option;
        }

        public override string ToString()
        {
            return Option.ToString();
        }
        
    }
}
