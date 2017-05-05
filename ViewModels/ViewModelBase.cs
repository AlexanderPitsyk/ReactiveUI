using ReactiveUI;

namespace ReactiveUIApplication.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IRoutableViewModel
    {
        protected ViewModelBase(string segmentName, IScreen hostScreen)
        {
            SegmentName = segmentName;
            HostScreen = hostScreen;
        }

        private string SegmentName { get; }

        public string UrlPathSegment => SegmentName;

        public IScreen HostScreen { get; }
    }
}