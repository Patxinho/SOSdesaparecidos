using SOSdesaparecidos.Models.Rss;
using SOSdesaparecidos.Services.Rss;
using SOSdesaparecidos.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSdesaparecidos.ViewModels.News
{
    public class NewsViewModel : ViewModelBase
    {

        private const string TWEET = "http://twitrss.me/twitter_user_to_rss/?user=sosdesaparecido&replies=on";

        private ObservableCollection<RSSFeedItem> _news;
        private RSSFeedItem _new;

        private IRssService _rssService;
        public NewsViewModel(
            IRssService rssService)
        {
            _rssService = rssService;
        }

        public ObservableCollection<RSSFeedItem> News
        {
            get { return _news; }
            set
            {
                _news = value;
                OnPropertyChanged();
            }
        }

        public RSSFeedItem SelectedNew
        {
            get { return _new; }
            set
            {
                _new = value;

                if (_new != null)
                {
                    //NavigationService.NavigateToAsync<NewDetailViewModel>(_new);
                }
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            var result = await _rssService.GetFeedAsync(TWEET);

            News = new ObservableCollection<RSSFeedItem>(result);
        }
    }
}
