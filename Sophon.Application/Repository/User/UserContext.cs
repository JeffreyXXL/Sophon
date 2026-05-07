using Sophon.Infrastructure;

namespace Sophon.Application
{
    [InjectableAttribute(DependencyLifetime.Singleton)]
    public class UserContext : IUserContext
    {
        public string CurrentUser { get; set; }
        public UserLevel CurrentLevel { get; set; } = UserLevel.None;

        public bool IsLoggedIn { get; set; } = false;

        private readonly IEventAggregator _eventAggregator;
        private readonly IUserRepository _userRepository;

        public UserContext(IEventAggregator eventAggregator, IUserRepository userRepository)
        {
            _eventAggregator = eventAggregator;
            _userRepository = userRepository;
            _eventAggregator.GetEvent<UserChangeEvent>().Subscribe(async userName =>
            {
                CurrentUser = userName;
                User user = await _userRepository.GetUserByName(CurrentUser);
                CurrentLevel = user == null ? UserLevel.None : _userRepository.GetLevelByUserName(CurrentUser);
                IsLoggedIn = user != null;
            });
        }
    }
}