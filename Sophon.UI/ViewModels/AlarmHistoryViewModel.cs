using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Sophon.Core;
using Sophon.Core.Event;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sophon.UI.ViewModels
{
    public class AlarmHistoryViewModel : BindableBase
    {
        private ObservableCollection<AlarmItem> _displayAlarmList;

        public ObservableCollection<AlarmItem> DisplayAlarmList
        {
            get { return _displayAlarmList; }
            set { SetProperty(ref _displayAlarmList, value); }
        }

        private bool _isFiltered;

        public bool IsFiltered
        {
            get { return _isFiltered; }
            set { SetProperty(ref _isFiltered, value); }
        }

        private DateTime _startTime;

        public DateTime StartTime
        {
            get { return _startTime; }
            set { SetProperty(ref _startTime, value); }
        }

        private DateTime _endTime;

        public DateTime EndTime
        {
            get { return _endTime; }
            set { SetProperty(ref _endTime, value); }
        }

        public DelegateCommand FilterCommand { get; private set; }

        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<AlarmItem> _historyAlarmList = new ObservableCollection<AlarmItem>();


        public AlarmHistoryViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<AlarmOccurredEvent>().Subscribe(x =>
            {
                _historyAlarmList.Add(x);
                DisplayAlarmList = IsFiltered ?
                               new ObservableCollection<AlarmItem>(_historyAlarmList.Where(a => a.Time > StartTime && a.Time < EndTime).ToList()) :
                               _historyAlarmList;
            }, ThreadOption.UIThread);

            FilterCommand = new DelegateCommand(ExcuteFilter);
        }


        private void ExcuteFilter()
        {
            IsFiltered = !IsFiltered;

            DisplayAlarmList = IsFiltered ?
                               new ObservableCollection<AlarmItem>(_historyAlarmList.Where(a => a.Time > StartTime && a.Time < EndTime).ToList()) :
                               _historyAlarmList;
        }


    }
}