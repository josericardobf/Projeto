package md55c7a6ad3a6d1804a4c558c629b7bea88;


public class SelecionarClienteActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Projeto.Cliente.SelecionarClienteActivity, Projeto, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SelecionarClienteActivity.class, __md_methods);
	}


	public SelecionarClienteActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SelecionarClienteActivity.class)
			mono.android.TypeManager.Activate ("Projeto.Cliente.SelecionarClienteActivity, Projeto, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
