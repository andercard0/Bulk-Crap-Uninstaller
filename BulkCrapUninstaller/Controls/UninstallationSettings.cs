﻿using System;
using System.Windows.Forms;
using BulkCrapUninstaller.Properties;
using Klocman.Binding;
using Klocman.Events;

namespace BulkCrapUninstaller.Controls
{
    public partial class UninstallationSettings : UserControl
    {
        private readonly SettingBinder<Settings> _settings = Settings.Default.SettingBinder;

        public UninstallationSettings()
        {
            InitializeComponent();

            _settings.BindControl(checkBoxShutdown, settings => settings.UninstallPreventShutdown, this);
            _settings.BindControl(checkBoxConcurrent, settings => settings.UninstallConcurrency, this);

            _settings.BindControl(checkBoxConcurrentOneLoud, settings => settings.UninstallConcurrentOneLoud, this);
            _settings.BindControl(checkBoxManualNoCollisionProtection, settings => settings.UninstallConcurrentDisableManualCollisionProtection, this);
            _settings.BindControl(checkBoxConcurrentLessCollisionProtection, settings => settings.UninstallConcurrentLessCollisionProtection, this);
            
            _settings.Subscribe(OnMaxCountChanged, settings => settings.UninstallConcurrentMaxCount, this);
            numericUpDownMaxConcurrent.ValueChanged += NumericUpDownMaxConcurrentOnValueChanged;

            _settings.SendUpdates(this);
            Disposed += (x, y) => _settings.RemoveHandlers(this);
        }

        private void NumericUpDownMaxConcurrentOnValueChanged(object sender, EventArgs eventArgs)
        {
            _settings.Settings.UninstallConcurrentMaxCount = (int)numericUpDownMaxConcurrent.Value;
        }

        private void OnMaxCountChanged(object sender, SettingChangedEventArgs<int> args)
        {
            numericUpDownMaxConcurrent.Value = args.NewValue;
        }
    }
}
