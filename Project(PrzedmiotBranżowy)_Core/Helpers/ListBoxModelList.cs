using Project_PrzedmiotBranzowy__Core.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PrzedmiotBranzowy__Core.Helpers
{
    public class ListBoxModelList<T, F> : BindableBase, IListBoxModel
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        private T _value;
        public T Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        private ObservableCollection<ListBoxModel<F>> _listBoxModels;
        public ObservableCollection<ListBoxModel<F>> ListBoxModels
        {
            get { return _listBoxModels; }
            set { SetProperty(ref _listBoxModels, value); }
        }

        public ListBoxModelList(T value)
        {
            _value = value;

            ListBoxModels = new();
        }

        public void InitListBoxModels(IEnumerable<F> models)
        {
            ListBoxModels.AddRange(models.Select((x) => new ListBoxModel<F>(x)));
        }
    }
}
