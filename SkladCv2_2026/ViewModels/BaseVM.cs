using System;
using DataEntity.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Material = DataEntity.Data.Material;

namespace SkladCv2_2026.ViewModels
{
    public abstract class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Material>? materialyCol;
        public ObservableCollection<Material> MaterialyCol
        {
            get
            {
                if (materialyCol == null)
                {
                    materialyCol = new ObservableCollection<Material>(SkladCv2_2026.Globals.Globals.context.Material);
                }
                return materialyCol;
            }
        }
    }
}
