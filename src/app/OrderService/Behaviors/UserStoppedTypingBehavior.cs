﻿using Plugin.InputKit.Shared.Controls;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OrderService.Behaviors
{
    public class UserStoppedTypingBehavior : BehaviorBase<AdvancedEntry>
    {
        private CancellationTokenSource? _tokenSource;

        #region Bindable Properties

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(UserStoppedTypingBehavior));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty StoppedTypingThresholdProperty = BindableProperty.Create(nameof(StoppedTypingThreshold), typeof(int), typeof(UserStoppedTypingBehavior), defaultValue: 1000);

        public int StoppedTypingThreshold
        {
            get => (int)GetValue(StoppedTypingThresholdProperty);
            set => SetValue(StoppedTypingThresholdProperty, value);
        }

        public static readonly BindableProperty AutoDismissKeyboardProperty = BindableProperty.Create(nameof(AutoDismissKeyboard), typeof(bool), typeof(UserStoppedTypingBehavior), defaultValue: false);

        public bool AutoDismissKeyboard
        {
            get => (bool)GetValue(AutoDismissKeyboardProperty);
            set => SetValue(AutoDismissKeyboardProperty, value);
        }

        #endregion Bindable Properties

        protected override void OnAttachedTo(AdvancedEntry inputView)
        {
            base.OnAttachedTo(inputView);

            inputView.TextChanged += InputView_TextChanged;
        }

        protected override void OnDetachingFrom(AdvancedEntry inputView)
        {
            base.OnDetachingFrom(inputView);

            inputView.TextChanged -= InputView_TextChanged;
        }

        private void InputView_TextChanged(object sender, TextChangedEventArgs e)
        {
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            PerformTextChanged((sender as InputView)!, e.NewTextValue, _tokenSource.Token);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private async Task PerformTextChanged(InputView inputView, string newTextValue, CancellationToken token)
        {
            await Task.Delay(StoppedTypingThreshold);

            if (token.IsCancellationRequested) return;

            if (Command != null && Command.CanExecute(newTextValue))
            {
                Command.Execute(newTextValue);

                if (AutoDismissKeyboard)
                {
                    inputView.Unfocus();
                }
            }
        }
    }
}