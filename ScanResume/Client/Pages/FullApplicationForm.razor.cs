using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScanResume.Client.Pages
{
    public partial class FullApplicationForm : ComponentBase
    {
        private CurrentFrom _currentFrom = CurrentFrom.Upload;

        private void ChangeForm(CurrentFrom newForm)
        {
            _currentFrom = newForm;
        }

        private string IsActiveForm(CurrentFrom form)
        {
            if (_currentFrom == form)
                return "is-active";
            return "";
        }
    }

    enum CurrentFrom
    {
        Upload, PersonalInfo, Work, Education, Other, Summary
    }
}
