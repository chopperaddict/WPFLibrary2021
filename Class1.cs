using System;
using System . Collections . Generic;
using System . Configuration;
using System . Diagnostics;
using System . Linq;
using System . Runtime;
using System . Text;
using System . Threading;
using System . Threading . Tasks;
using System . Windows;
using System . Windows . Controls;
using System . Windows . Controls . Primitives;
using System . Windows . Input;
using System . Windows . Media;
using System . Windows . Media . Imaging;

using Microsoft . Win32;

using NuGet . Configuration;

namespace WPFLibrary2021
{
	public class Library1
	{
//		public static Action<DataGrid, int> GridInitialSetup = Library1 . SetUpGridSelection;
		//		public static Func<bool, BankAccountViewModel, CustomerViewModel, DetailsViewModel> IsMatched = CheckRecordMatch; 
//		public static Func<object, object, bool> IsRecordMatched = Library1 . CompareDbRecords;

		public struct bankrec
		{
			public string custno
			{
				get; set;
			}
			public string bankno
			{
				get; set;
			}
			public int actype
			{
				get; set;
			}
			public decimal intrate
			{
				get; set;
			}
			public decimal balance
			{
				get; set;
			}
			public DateTime odate
			{
				get; set;
			}
			public DateTime cdate
			{
				get; set;
			}
		}


		// Declare the first few notes of the song, "Mary Had A Little Lamb".
		// Define the frequencies of notes in an octave, as well as
		// silence (rest).
		protected enum Tone
		{
			REST = 0,
			GbelowC = 196,
			A = 220,
			Asharp = 233,
			B = 247,
			C = 262,
			Csharp = 277,
			D = 294,
			Dsharp = 311,
			E = 330,
			F = 349,
			Fsharp = 370,
			G = 392,
			Gsharp = 415,
		}

		// Define the duration of a note in units of milliseconds.
		protected enum Duration
		{
			WHOLE = 1600,
			HALF = WHOLE / 2,
			QUARTER = HALF / 2,
			EIGHTH = QUARTER / 2,
			SIXTEENTH = EIGHTH / 2,
		}

		protected struct Note
		{
			Tone toneVal;
			Duration durVal;

			// Define a constructor to create a specific note.
			public Note ( Tone frequency, Duration time )
			{
				toneVal = frequency;
				durVal = time;
			}

			// Define properties to return the note's tone and duration.
			public Tone NoteTone
			{
				get
				{
					return toneVal;
				}
			}
			public Duration NoteDuration
			{
				get
				{
					return durVal;
				}
			}
		}
		public static void PlayMary ( )
		{
			Note [ ] Mary =
			{
					new Note(Tone.B, Duration.QUARTER),
					new Note(Tone.A, Duration.QUARTER),
					new Note(Tone.GbelowC, Duration.QUARTER),
					new Note(Tone.A, Duration.QUARTER),
					new Note(Tone.B, Duration.QUARTER),
					new Note(Tone.B, Duration.QUARTER),
					new Note(Tone.B, Duration.HALF),
					new Note(Tone.A, Duration.QUARTER),
					new Note(Tone.A, Duration.QUARTER),
					new Note(Tone.A, Duration.HALF),
					new Note(Tone.B, Duration.QUARTER),
					new Note(Tone.D, Duration.QUARTER),
					new Note(Tone.D, Duration.HALF)
			};
			// Play the song
			Play ( Mary );
		}
		// Play the notes in a song.
		protected static void Play ( Note [ ] tune )
		{
			foreach ( Note n in tune )
			{
				if ( n . NoteTone == Tone . REST )
					Thread . Sleep ( ( int ) n . NoteDuration );
				else
					Console . Beep ( ( int ) n . NoteTone, ( int ) n . NoteDuration );
			}
		}

		//Generic form of Selection forcing code below
		/// <summary>
		/// This is a great method that almost guarantees to 
		/// highlight the selected item in any datagrid.
		/// Found on StackOverflow (of course)
		/// </summary>
		/// <param name="dgrid"></param>
		/// <param name="index"></param>
		public static void SetGridRowSelectionOn ( DataGrid dgrid, int index )
		{
			if ( dgrid . Items . Count > 0 && index != -1 )
			{
				try
				{

					dgrid . SelectedIndex = index;
					dgrid . SelectedItem = index;
					dgrid . UpdateLayout ( );
					dgrid . ScrollIntoView ( dgrid . Items [ index ] );
					DataGridRow r = dgrid . ItemContainerGenerator . ContainerFromIndex ( index ) as DataGridRow;
					if ( r != null )
					{
						r . IsSelected = false;
						r . IsSelected = true;
					}
				}
				catch ( Exception ex )
				{
					Debug . WriteLine ( $"{ex . Message}, {ex . Data}" );
				}
			}
		}
		public static string convertToHex ( double temp )
		{
			int intval = ( int ) Convert . ToInt32 ( temp );
			string hexval = intval . ToString ( "X" );
			return hexval;
		}

		#region BRUSHES SUPPORT
		//Working well 4/8/21
		/// <summary>
		/// Accepts color in Colors.xxxx format = "Blue" etc
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static Brush BrushFromColors ( Color color )
		{
			Brush brush = new SolidColorBrush ( color );
			return brush;
		}
		//Working well 4/8/21
		/// <summary>
		/// Accpets string in "#XX00FF00" or similar
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static Brush BrushFromHashString ( string color )
		{
			//Must start with  '#'
			string s = color . ToString ( );
			if ( !s . Contains ( "#" ) )
				return Library1 . BrushFromColors ( Colors . Transparent );
			Brush brush = ( Brush ) new BrushConverter ( ) . ConvertFromString ( color );
			return brush;
		}

		#endregion BRUSHES SUPPORT


		public static void AddUpdateAppSettings ( string key, string value )
		{
			try
			{
				var configFile = ConfigurationManager . OpenExeConfiguration ( ConfigurationUserLevel . None );
				var settings = configFile . AppSettings . Settings;
				if ( settings [ key ] == null )
				{
					settings . Add ( key, value );
				}
				else
				{
					settings [ key ] . Value = value;
				}
				configFile . Save ( ConfigurationSaveMode . Full );
				ConfigurationManager . RefreshSection ( configFile . AppSettings . SectionInformation . Name );
			}
			catch ( ConfigurationErrorsException )
			{
				Console . WriteLine ( "Error writing app settings" );
			}
		}

		public static string ReadConfigSetting ( string key )
		{
			string result = "";
			try
			{
				var appSettings = ConfigurationManager . AppSettings;
				result = appSettings [ key ] ?? "Not Found";
				Console . WriteLine ( result );
			}
			catch ( ConfigurationErrorsException )
			{
				Console . WriteLine ( "Error reading app settings" );
			}
			return result;
		}
		public static void ReadAllConfigSettings ( )
		{
			try
			{
				var appSettings = ConfigurationManager . AppSettings;

				if ( appSettings . Count == 0 )
				{
					Console . WriteLine ( "AppSettings is empty." );
				}
				else
				{
					foreach ( var key in appSettings . AllKeys )
					{
						Console . WriteLine ( "Key: {0} Value: {1}", key, appSettings [ key ] );
					}
				}
			}
			catch ( ConfigurationErrorsException )
			{
				Console . WriteLine ( "Error reading app settings" );
			}
		}
		public static string GetExportFileName ( string filespec )
		// opens  the common file open dialog
		{
			OpenFileDialog ofd = new OpenFileDialog ( );
			ofd . InitialDirectory = @"C:\Users\ianch\Documents\";
			ofd . CheckFileExists = false;
			ofd . AddExtension = true;
			ofd . Title = "Select name for Exported data file.";
			if ( filespec . ToUpper ( ) . Contains ( "XL" ) )
				ofd . Filter = "Excel Spreadsheets (*.xl*) | *.xl*";
			else if ( filespec . ToUpper ( ) . Contains ( "CSV" ) )
				ofd . Filter = "Comma seperated data (*.csv) | *.csv";
			else if ( filespec . ToUpper ( ) . Contains ( "*.*" ) )
				ofd . Filter = "All Files (*.*) | *.*";
			else if ( filespec == "" )
			{
				ofd . Filter = "All Files (*.*) | *.*";
				ofd . DefaultExt = ".CSV";
			}
			ofd . FileName = filespec;
			ofd . ShowDialog ( );
			string fnameonly = ofd . SafeFileName;
			return ofd . FileName;
		}

		public static string GetImportFileName ( string filespec )
		// opens  the common file open dialog
		{
			OpenFileDialog ofd = new OpenFileDialog ( );
			ofd . InitialDirectory = @"C:\Users\ianch\Documents\";
			ofd . CheckFileExists = true;
			if ( filespec . ToUpper ( ) . Contains ( "XL" ) )
				ofd . Filter = "Excel Spreadsheets (*.xl*) | *.xl*";
			else if ( filespec . ToUpper ( ) . Contains ( "CSV" ) )
				ofd . Filter = "Comma seperated data (*.csv) | *.csv";
			else if ( filespec . ToUpper ( ) . Contains ( "*.*" ) || filespec == "" )
				ofd . Filter = "All Files (*.*) | *.*";
			ofd . AddExtension = true;
			ofd . ShowDialog ( );
			return ofd . FileName;
		}
		public static string ConvertInputDate ( string datein )
		{
			string YYYMMDD = "";
			string [ ] datebits;
			// This filter will strip off the "Time" section of an excel date
			// and return us a valid YYYY/MM/DD string
			char [ ] ch = { '/', ' ' };
			datebits = datein . Split ( ch );
			if ( datebits . Length < 3 )
				return datein;

			// check input to see if it needs reversing ?
			if ( datebits [ 0 ] . Length == 4 )
				YYYMMDD = datebits [ 0 ] + "/" + datebits [ 1 ] + "/" + datebits [ 2 ];
			else
				YYYMMDD = datebits [ 2 ] + "/" + datebits [ 1 ] + "/" + datebits [ 0 ];
			return YYYMMDD;
		}


		public static void SetSelectedItemFirstRow ( object dataGrid, object selectedItem )
		{
			//If target datagrid Empty, throw exception
			if ( dataGrid == null )
			{
				throw new ArgumentNullException ( "Target none" + dataGrid + "Cannot convert to DataGrid" );
			}
			//Get target DataGrid，If it is empty, an exception will be thrown
			System . Windows . Controls . DataGrid dg = dataGrid as System . Windows . Controls . DataGrid;
			if ( dg == null )
			{
				throw new ArgumentNullException ( "Target none" + dataGrid + "Cannot convert to DataGrid" );
			}
			//If the data source is empty, return
			if ( dg . Items == null || dg . Items . Count < 1 )
			{
				return;
			}

			dg . SelectedItem = selectedItem;
			dg . CurrentColumn = dg . Columns [ 0 ];
			dg . ScrollIntoView ( dg . SelectedItem, dg . CurrentColumn );
		}
		public static bool DataGridHasFocus ( DependencyObject instance )
		{
			//how to fibnd out whether a datagrid has focus or not to handle key previewers
			IInputElement focusedControl = FocusManager . GetFocusedElement ( instance );
			if ( focusedControl == null )
				return true;
			string compare = focusedControl . ToString ( );
			if ( compare . ToUpper ( ) . Contains ( "DATAGRID" ) )
				return true;
			else
				return false;
		}
		public static void GetWindowHandles ( )
		{
#if SHOWWINDOWDATA
			Console . WriteLine ( $"Current Windows\r\n" + "===============" );
			foreach ( Window window in System . Windows . Application . Current . Windows )
			{
				if ( ( string ) window . Title != "" && ( string ) window . Content != "" )
				{
					Console . WriteLine ( $"Title:  {window . Title },\r\nContent - {window . Content}" );
					Console . WriteLine ( $"Name = [{window . Name}]\r\n" );
				}
			}
#endif
		}
		public static bool FindWindowFromTitle ( string searchterm, ref Window handle )
		{
			bool result = false;
			foreach ( Window window in System . Windows . Application . Current . Windows )
			{
				if ( window . Title . ToUpper ( ) . Contains ( searchterm . ToUpper ( ) ) )
				{
					handle = window;
					result = true;
					break;
				}
			}
			return result;
		}

		//************************************************************************************//
		public static Style GetDictionaryStyle ( string tempname )
		{
			Style ctmp = System . Windows . Application . Current . FindResource ( tempname ) as Style;
			return ctmp;
		}
		public static ControlTemplate GetDictionaryControlTemplate ( string tempname )
		{
			ControlTemplate ctmp = System . Windows . Application . Current . FindResource ( tempname ) as ControlTemplate;
			return ctmp;
		}
		//************************************************************************************//
		public static Brush GetDictionaryBrush ( string brushname )
		{
			Brush brs = null;
			try
			{
				brs = System . Windows . Application . Current . FindResource ( brushname ) as Brush;
			}
			catch
			{

			}
			return brs;
		}
		// Utility functions for sensing scrollbars when dragging from a grid etc
		public static bool HitTestScrollBar ( object sender, MouseButtonEventArgs e )
		{
			//			HitTestResult hit = VisualTreeHelper . HitTest ( ( Visual ) sender, e . GetPosition ( ( IInputElement ) sender ) );
			//			return hit . VisualHit . GetVisualAncestor<ScrollBar> ( ) != null;
			object original = e . OriginalSource;
			try
			{
				if ( !original . GetType ( ) . Equals ( typeof ( ScrollBar ) ) )
				{
					if ( original . GetType ( ) . Equals ( typeof ( DataGrid ) ) )
					{
						Console . WriteLine ( "DataGrid is clicked" );
					}
					else if ( FindVisualParent<ScrollBar> ( original as DependencyObject ) != null )
					{
						//scroll bar is clicked
						return true;
					}
					return false;
					;
				}
			}
			catch ( Exception ex )
			{
				Debug . WriteLine ( $"Error in HitTest ScriollBar Function (Utils-1010({ex . Data}" );
			}
			return true;
		}
		public static bool HitTestHeaderBar ( object sender, MouseButtonEventArgs e )
		{
			//			HitTestResult hit = VisualTreeHelper . HitTest ( ( Visual ) sender, e . GetPosition ( ( IInputElement ) sender ) );
			//			return hit . VisualHit . GetVisualAncestor<ScrollBar> ( ) != null;
			object original = e . OriginalSource;

			if ( !original . GetType ( ) . Equals ( typeof ( DataGridColumnHeader ) ) )
			{
				if ( original . GetType ( ) . Equals ( typeof ( DataGrid ) ) )
				{
					Console . WriteLine ( "DataGrid is clicked" );
				}
				else if ( FindVisualParent<DataGridColumnHeader> ( original as DependencyObject ) != null )
				{
					//Header bar is clicked
					return true;
				}
				return false;
				;
			}
			return true;
		}
		public static parentItem FindVisualParent<parentItem> ( DependencyObject obj ) where parentItem : DependencyObject
		{
			DependencyObject parent = VisualTreeHelper . GetParent ( obj );
			while ( parent != null && !parent . GetType ( ) . Equals ( typeof ( parentItem ) ) )
			{
				parent = VisualTreeHelper . GetParent ( parent );
			}
			return parent as parentItem;
		}
		public static T FindVisualChildByName<T> ( DependencyObject parent, string name ) where T : DependencyObject
		{
			for ( int i = 0 ; i < VisualTreeHelper . GetChildrenCount ( parent ) ; i++ )
			{
				var child = VisualTreeHelper . GetChild ( parent, i );
				string controlName = child . GetValue ( Control . NameProperty ) as string;
				if ( controlName == name )
				{
					return child as T;
				}
				else
				{
					T result = FindVisualChildByName<T> ( child, name );
					if ( result != null )
						return result;
				}
			}
			return null;
		}
		/// <summary>
		/// a different version of FindChild - find by child NAME (string)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parent"></param>
		/// <param name="childName"></param>
		/// <returns></returns>
		public static T FindChild<T> ( DependencyObject parent, string childName )
			where T : DependencyObject
		{
			// Confirm parent and childName are valid. 
			if ( parent == null )
				return null;
			T foundChild = null;
			int childrenCount = VisualTreeHelper . GetChildrenCount ( parent );
			for ( int i = 0 ; i < childrenCount ; i++ )
			{
				var child = VisualTreeHelper . GetChild ( parent, i );
				// If the child is not of the request child type child
				T childType = child as T;
				if ( childType == null )
				{
					// recursively drill down the tree
					foundChild = FindChild<T> ( child, childName );
					// If the child is found, break so we do not overwrite the found child. 
					if ( foundChild != null )
						break;
				}
				else if ( !string . IsNullOrEmpty ( childName ) )
				{
					var frameworkElement = child as FrameworkElement;
					// If the child's name is set for search
					if ( frameworkElement != null && frameworkElement . Name == childName )
					{
						// if the child's name is of the request name
						foundChild = ( T ) child;
						break;
					}
				}
				else
				{
					// child element found.
					foundChild = ( T ) child;
					break;
				}
			}
			return foundChild;
		}

		/// <summary>
		/// a different version of FindChild - find by object TYPE
		/// </summary>
		/// <param name="o"></param>
		/// <param name="childType"></param>
		/// <returns></returns>
		public static DependencyObject FindChild ( DependencyObject o, Type childType )
		{
			DependencyObject foundChild = null;
			if ( o != null )
			{
				int childrenCount = VisualTreeHelper . GetChildrenCount ( o );
				for ( int i = 0 ; i < childrenCount ; i++ )
				{
					var child = VisualTreeHelper . GetChild ( o, i );
					if ( child . GetType ( ) != childType )
					{
						foundChild = FindChild ( child, childType );
						//if(foundChild == null)
						//        FindChild ( child, childType );
					}
					else
					{
						foundChild = child;
						break;
					}
				}
			}
			return foundChild;
		}
		public static string GetPrettyGridStatistics ( DataGrid Grid, int current )
		{
			string output = "";
			if ( current != -1 )
				output = $"{current} / {Grid . Items . Count}";
			else
				output = $"0 / {Grid . Items . Count}";
			return output;
		}
		/// <summary>
		/// Handles the making of any window to be draggable via a simple click/Drag inside them
		/// Very useful method
		/// </summary>
		/// <param name="inst"></param>
		public static void SetupWindowDrag ( Window inst )
		{
			inst . MouseDown += delegate
			{
				try
				{
					inst . DragMove ( );
				}
				catch { return; }
			};
		}

		/// <summary>
		/// Creates a BMP from any control passed into it
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public static RenderTargetBitmap RenderBitmap ( FrameworkElement element )
		{
			double topLeft = 0;
			double topRight = 0;
			int width = ( int ) element . ActualWidth;
			int height = ( int ) element . ActualHeight;
			double dpiX = 96; // this is the magic number
			double dpiY = 96; // this is the magic number

			PixelFormat pixelFormat = PixelFormats . Default;
			VisualBrush elementBrush = new VisualBrush ( element );
			DrawingVisual visual = new DrawingVisual ( );
			DrawingContext dc = visual . RenderOpen ( );

			dc . DrawRectangle ( elementBrush, null, new Rect ( topLeft, topRight, width, height ) );
			dc . Close ( );

			RenderTargetBitmap bitmap = new RenderTargetBitmap ( width, height, dpiX, dpiY, pixelFormat );

			bitmap . Render ( visual );
			return bitmap;
		}

		// Utilities to support converters
		#region UTILITIES
		//public class ConverterUtils
		//{

		public static Brush GetBrushFromInt ( int value )
		{
			switch ( value )
			{
				case 0:
					return ( Brushes . White );
				case 1:
					return ( Brushes . Yellow );
				case 2:
					return ( Brushes . Orange );
				case 3:
					return ( Brushes . Red );
				case 4:
					return ( Brushes . Magenta );
				case 5:
					return ( Brushes . Gray );
				case 6:
					return ( Brushes . Aqua );
				case 7:
					return ( Brushes . Azure );
				case 8:
					return ( Brushes . Brown );
				case 9:
					return ( Brushes . Crimson );
				case 10:
					return ( Brushes . Transparent );
			}
			return ( Brush ) null;
		}
		public static Brush GetBrush ( string parameter )
		{
			if ( parameter == "BLUE" )
				return Brushes . Blue;
			else if ( parameter == "RED" )
				return Brushes . Red;
			else if ( parameter == "GREEN" )
				return Brushes . Green;
			else if ( parameter == "CYAN" )
				return Brushes . Cyan;
			else if ( parameter == "MAGENTA" )
				return Brushes . Magenta;
			else if ( parameter == "YELLOW" )
				return Brushes . Yellow;
			else if ( parameter == "WHITE" )
				return Brushes . White;
			else
			{
				//We appear to have received a Brushes Resource Name, so return that Brushes value
				Brush b = ( Brush ) Library1 . GetDictionaryBrush ( parameter . ToString ( ) );
				return b;
			}
		}
		/// <summary>
		/// Returns a control that is accessible in code behind from a template control
		/// </summary>
		/// <param name="RectBtn"></param>
		/// <param name="CtrlName"></param>
		/// <returns></returns>
		public static object GetTemplateControl ( Control RectBtn, string CtrlName )
		{
			var template = RectBtn . Template;
			object v = template . FindName ( CtrlName, RectBtn ) as object;
			return v;
		}


		//}
		#endregion UTILITIES


	}
}
