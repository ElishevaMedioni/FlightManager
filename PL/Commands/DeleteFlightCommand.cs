using PL.Historic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PL.Commands
{
    class DeleteFlightCommand:ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            HistoricViewModel historicViewModel = parameter as HistoricViewModel;
            historicViewModel.HistoricFlights.Clear();
            var flight = historicViewModel.GetFlightsHistoricViewModel();
            historicViewModel.DeleteFlightsHistoricViewModel(flight);
        }

        public HistoricViewModel historicVM { get; set; }

        public DeleteFlightCommand(HistoricViewModel currentVM)
        {
            historicVM = currentVM;
        }


    }
}
