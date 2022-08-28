using ReactiveUI;
using System.Windows.Forms;

namespace RxUi_Serilog;

public partial class ShellView : Form, IViewFor<ShellViewModel>
{
	public ShellViewModel ViewModel { get; set; }

	object IViewFor.ViewModel
	{
		get => ViewModel;
		set => ViewModel = (ShellViewModel)value;
	}

	public ShellView()
	{
		InitializeComponent();

		this.WhenActivated(disposableRegistration =>
		{

		});

	}

}
