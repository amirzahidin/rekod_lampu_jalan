package crc64e05ffe158a345152;


public class ClusterLogicListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.maps.android.clustering.ClusterManager.OnClusterClickListener,
		com.google.maps.android.clustering.ClusterManager.OnClusterItemClickListener,
		com.google.maps.android.clustering.ClusterManager.OnClusterInfoWindowClickListener,
		com.google.maps.android.clustering.ClusterManager.OnClusterItemInfoWindowClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onClusterClick:(Lcom/google/maps/android/clustering/Cluster;)Z:GetOnClusterClick_Lcom_google_maps_android_clustering_Cluster_Handler:Android.Gms.Maps.Utils.Clustering.ClusterManager/IOnClusterClickListenerInvoker, Google.Maps.Utils.Android\n" +
			"n_onClusterItemClick:(Lcom/google/maps/android/clustering/ClusterItem;)Z:GetOnClusterItemClick_Lcom_google_maps_android_clustering_ClusterItem_Handler:Android.Gms.Maps.Utils.Clustering.ClusterManager/IOnClusterItemClickListenerInvoker, Google.Maps.Utils.Android\n" +
			"n_onClusterInfoWindowClick:(Lcom/google/maps/android/clustering/Cluster;)V:GetOnClusterInfoWindowClick_Lcom_google_maps_android_clustering_Cluster_Handler:Android.Gms.Maps.Utils.Clustering.ClusterManager/IOnClusterInfoWindowClickListenerInvoker, Google.Maps.Utils.Android\n" +
			"n_onClusterItemInfoWindowClick:(Lcom/google/maps/android/clustering/ClusterItem;)V:GetOnClusterItemInfoWindowClick_Lcom_google_maps_android_clustering_ClusterItem_Handler:Android.Gms.Maps.Utils.Clustering.ClusterManager/IOnClusterItemInfoWindowClickListenerInvoker, Google.Maps.Utils.Android\n" +
			"";
		mono.android.Runtime.register ("Maui.GoogleMaps.Clustering.Platforms.Android.ClusterLogicListener, Maui.GoogleMaps.Clustering", ClusterLogicListener.class, __md_methods);
	}

	public ClusterLogicListener ()
	{
		super ();
		if (getClass () == ClusterLogicListener.class) {
			mono.android.TypeManager.Activate ("Maui.GoogleMaps.Clustering.Platforms.Android.ClusterLogicListener, Maui.GoogleMaps.Clustering", "", this, new java.lang.Object[] {  });
		}
	}

	public boolean onClusterClick (com.google.maps.android.clustering.Cluster p0)
	{
		return n_onClusterClick (p0);
	}

	private native boolean n_onClusterClick (com.google.maps.android.clustering.Cluster p0);

	public boolean onClusterItemClick (com.google.maps.android.clustering.ClusterItem p0)
	{
		return n_onClusterItemClick (p0);
	}

	private native boolean n_onClusterItemClick (com.google.maps.android.clustering.ClusterItem p0);

	public void onClusterInfoWindowClick (com.google.maps.android.clustering.Cluster p0)
	{
		n_onClusterInfoWindowClick (p0);
	}

	private native void n_onClusterInfoWindowClick (com.google.maps.android.clustering.Cluster p0);

	public void onClusterItemInfoWindowClick (com.google.maps.android.clustering.ClusterItem p0)
	{
		n_onClusterItemInfoWindowClick (p0);
	}

	private native void n_onClusterItemInfoWindowClick (com.google.maps.android.clustering.ClusterItem p0);

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
