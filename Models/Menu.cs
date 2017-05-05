using ReactiveUIApplication.Enums;

namespace ReactiveUIApplication.Models
{
    public class Menu
    {
        public Menu(MenuOption option)
        {
            Option = option;
        }

        public MenuOption Option { get; }

        public override string ToString()
        {
            return Option.ToString();
        }
    }
}