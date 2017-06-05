using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
//using UnityEditorInternal;

using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

using Flux;

namespace FluxEditor
{

	public static class FUtility
	{
		#region Preferences
		private static bool _preferencesLoaded = false;

		// keys
		private const string FLUX_KEY = "Flux.";

		private const string TIME_FORMAT_KEY = FLUX_KEY + "TimeFormat";
		private const string FRAME_RATE_KEY = FLUX_KEY + "FrameRate";
		private const string OPEN_SEQUENCE_ON_SELECT_KEY = FLUX_KEY + "OpenSequenceOnSelect";
		private const string RENDER_ON_EDITOR_PLAY_KEY = FLUX_KEY + "ReplainOnEditorPlay";

		// defaults
		private const TimeFormat DEFAULT_TIME_FORMAT = TimeFormat.Frames;
		private const int DEFAULT_FRAME_RATE = FSequence.DEFAULT_FRAMES_PER_SECOND;
		private const bool DEFAULT_OPEN_SEQUENCE_ON_SELECT = false;
		private const bool DEFAULT_RENDER_ON_EDITOR_PLAY = true;

		// current values
		private static TimeFormat _timeFormat = DEFAULT_TIME_FORMAT;
		public static TimeFormat TimeFormat { get { return _timeFormat; } }

		private static int _frameRate = DEFAULT_FRAME_RATE;
		public static int FrameRate { get { return _frameRate; } }

		private static bool _openSequenceOnSelect = DEFAULT_OPEN_SEQUENCE_ON_SELECT;
		public static bool OpenSequenceOnSelect { get { return _openSequenceOnSelect; } }

		private static bool _renderOnEditorPlay = DEFAULT_RENDER_ON_EDITOR_PLAY;
		public static bool RenderOnEditorPlay { get { return _renderOnEditorPlay; } }

		[PreferenceItem("Flux Editor")]
		public static void PreferencesGUI()
		{
			if( !_preferencesLoaded )
				LoadPreferences();

			EditorGUI.BeginChangeCheck();

			_timeFormat = (TimeFormat)EditorGUILayout.EnumPopup( new GUIContent("Time Format", "In what format is time represented? E.g. for 60fps:\n\tFrames: 127\n\tSeconds: 2.12\n\tSeconds Formatted: 2.7"), _timeFormat );
			_frameRate = EditorGUILayout.IntSlider( new GUIContent("Frame Rate", "How many frames per second have the Sequences by default?"), _frameRate, 1, 120 );
			_openSequenceOnSelect = EditorGUILayout.Toggle( new GUIContent("Open Sequence On Select", "Should it open Sequences in Flux Editor window when they are selected?"), _openSequenceOnSelect );
			_renderOnEditorPlay = EditorGUILayout.Toggle( new GUIContent("Render On Editor Play", "Render Flux Editor window when editor is in Play mode? Turn it off it you want to avoid the redraw costs of Flux when selected sequence is playing."),
			                                             _renderOnEditorPlay );

			if( EditorGUI.EndChangeCheck() )
				SavePreferences();
		}
		
		public static void LoadPreferences()
		{
			_preferencesLoaded = true;

			_timeFormat = (TimeFormat)EditorPrefs.GetInt( TIME_FORMAT_KEY, (int)DEFAULT_TIME_FORMAT );
			_frameRate = EditorPrefs.GetInt( FRAME_RATE_KEY, DEFAULT_FRAME_RATE );
			_openSequenceOnSelect = EditorPrefs.GetBool( OPEN_SEQUENCE_ON_SELECT_KEY, DEFAULT_OPEN_SEQUENCE_ON_SELECT );
			_renderOnEditorPlay = EditorPrefs.GetBool( RENDER_ON_EDITOR_PLAY_KEY, DEFAULT_RENDER_ON_EDITOR_PLAY );
		}

		public static void SavePreferences()
		{
			EditorPrefs.SetInt( TIME_FORMAT_KEY, (int)_timeFormat );
			EditorPrefs.SetInt( FRAME_RATE_KEY, _frameRate );
			EditorPrefs.SetBool( OPEN_SEQUENCE_ON_SELECT_KEY, _openSequenceOnSelect );
			EditorPrefs.SetBool( RENDER_ON_EDITOR_PLAY_KEY, _renderOnEditorPlay );
		}

		#endregion Preferences

		#region Paths / Resource Loading

		private static string _fluxPath = null;
		private static string _fluxEditorPath = null;
		private static string _fluxSkinPath = null;

		private static EditorWindow _gameView = null;

		private static GUISkin _fluxSkin = null;
		private static GUIStyle _evtStyle = null;
//		private static GUIStyle _commentEvtStyle = null;

		public static string FindFluxDirectory()
		{
			string[] directories = Directory.GetDirectories("Assets", "Flux", SearchOption.AllDirectories);
			return directories.Length > 0 ? directories[0] : string.Empty;
		}

		
		public static string GetFluxPath()
		{
			if( _fluxPath == null )
			{
				_fluxPath = FindFluxDirectory()+'/';
				_fluxEditorPath = _fluxPath + "Editor/";
				_fluxSkinPath = _fluxEditorPath + "Skin/";
			}
			return _fluxPath;
		}
		
		public static string GetFluxEditorPath()
		{
			if( _fluxEditorPath == null ) GetFluxPath();
			return _fluxEditorPath;
		}
		
		public static string GetFluxSkinPath()
		{
			if( _fluxSkinPath == null ) GetFluxPath();
			return _fluxSkinPath;
		}

		public static GUISkin GetFluxSkin()
		{
			if( _fluxSkin == null )
				_fluxSkin = (GUISkin)AssetDatabase.LoadAssetAtPath( GetFluxSkinPath()+"FSkin.guiskin", typeof(GUISkin) );
			return _fluxSkin;
		}

		public static Texture2D GetFluxTexture( string textureFile )
		{
			return (Texture2D)AssetDatabase.LoadAssetAtPath( GetFluxSkinPath() + textureFile, typeof(Texture2D) );
		}

		public static GUIStyle GetEventStyle()
		{
			if( _evtStyle == null )
				_evtStyle = GetFluxSkin().GetStyle("Event");
			return _evtStyle;
		}

		public static GUIStyle GetCommentEventStyle()
		{
			return GetEventStyle();
//			if( _commentEvtStyle == null )
//				_commentEvtStyle = GetFluxSkin().GetStyle("CommentEvent");
//			return _commentEvtStyle;
		}

		private static FSettings _settings = null;

		public static Color GetEventColor( string eventTypeStr )
		{
			return GetSettings().GetEventColor( eventTypeStr );
		}

		public static FSettings GetSettings()
		{
			if( _settings == null )
				_settings = (FSettings)AssetDatabase.LoadAssetAtPath( GetFluxEditorPath() + "FluxSettings.asset", typeof(FSettings) );
			return _settings;
		}

		#endregion Paths / Resource Loading

		#region Events

//		public static void Resize( FEvent evt, FrameRange newFrameRange )
//		{
//			if( evt.FrameRange == newFrameRange || newFrameRange.Start > newFrameRange.End )
//				return;
//			
//			if( newFrameRange.Length < evt.GetMinLength() || newFrameRange.Length > evt.GetMaxLength() )
//			{
//				Debug.LogError( string.Format("Trying to resize an Event to [{0},{1}] (length: {2}) which isn't a valid length, should be between [{3},{4}]", newFrameRange.Start, newFrameRange.End, newFrameRange.Length, evt.GetMinLength(), evt.GetMaxLength() ), evt );
//				return;
//			}
//			
//			bool changedLength = evt.Length != newFrameRange.Length;
//
//			if( !evt.GetTrack().CanAdd( evt, newFrameRange ) )
//				return;
//
//			Undo.RecordObject( evt, changedLength ? "Resize Event" : "Move Event" );
//			
//			evt.Start = newFrameRange.Start;
//			evt.End = newFrameRange.End;
//			
//			if( changedLength )
//			{
//				if( evt is FPlayAnimationEvent )
//				{
//					FPlayAnimationEvent animEvt = (FPlayAnimationEvent)evt;
//					
//					if( animEvt.IsAnimationEditable() )
//					{
//						FAnimationEventInspector.ScaleAnimationClip( animEvt._animationClip, animEvt.FrameRange );
//					}
//				}
//				else if( evt is FTimescaleEvent )
//				{
//					ResizeAnimationCurve( (FTimescaleEvent)evt, newFrameRange );
//				}
//			}
//			
//			EditorUtility.SetDirty( evt );
//			
//			if( FSequenceEditorWindow.instance != null )
//				FSequenceEditorWindow.instance.Repaint();
//		}
		
		public static void Rescale( FEvent evt, FrameRange newFrameRange )
		{
			if( evt.FrameRange == newFrameRange )
				return;
			
			Undo.RecordObject( evt, string.Empty );
			
			evt.Start = newFrameRange.Start;
			evt.End = newFrameRange.End;
			
			if( evt is FPlayAnimationEvent )
			{
				FPlayAnimationEvent animEvt = (FPlayAnimationEvent)evt;
				
				if( animEvt._animationClip != null )
				{
					if( Flux.FUtility.IsAnimationEditable(animEvt._animationClip) )
					{
						animEvt._animationClip.frameRate = animEvt.Sequence.FrameRate;
						EditorUtility.SetDirty( animEvt._animationClip );
					}
					else if( Mathf.RoundToInt(animEvt._animationClip.frameRate) != animEvt.Sequence.FrameRate )
					{
						Debug.LogError( string.Format("Removed AnimationClip '{0}' ({1} fps) from Animation Event '{2}'", 
						                              animEvt._animationClip.name, 
						                              animEvt._animationClip.frameRate, 
						                              animEvt.name ), animEvt );
						
						animEvt._animationClip = null;
					}
				}
			}
			
			EditorUtility.SetDirty( evt );
		}

		#endregion Events


		public static void RepaintGameView()
		{
			if( _gameView == null )
				_gameView = EditorWindow.GetWindow(typeof(EditorWindow).Assembly.GetType( "UnityEditor.GameView" ));

			_gameView.Repaint();
			SceneView.RepaintAll();
//			UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
		}

		public static string GetTime( int frame, int frameRate )
		{
			switch( _timeFormat )
			{
			case TimeFormat.Frames:
				return frame.ToString();
			case TimeFormat.Seconds:
				return ((float)frame / frameRate).ToString("0.##");
			case TimeFormat.SecondsFormatted:
				return string.Format( "{0}:{1}", frame / frameRate, frame % frameRate );
			}

			return frame.ToString();
		}

		#region Audio Utilities

		public static Texture2D GetAudioClipTexture( AudioClip clip, float width, float height )
		{
			if( clip == null )
				return null;

			AudioImporter importer = (AudioImporter)AssetImporter.GetAtPath( AssetDatabase.GetAssetPath( clip ) );

			MethodInfo getWaveForm = typeof(Editor).Assembly.GetType("UnityEditor.AudioUtil").GetMethod("GetWaveForm", 
			                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod );

			Texture2D[] waveForms = new Texture2D[clip.channels]; 
			for( int channel = 0; channel < clip.channels; ++channel )
				waveForms[channel] = (Texture2D)getWaveForm.Invoke( null, new object[]{clip, importer, channel, width, height / clip.channels});
//				waveForms[channel] = AudioUtil.GetWaveForm(clip, importer, channel, width, height / clip.channels);
			return CombineWaveForms(waveForms);
		}

		public static Texture2D CombineWaveForms( Texture2D[] waveForms )
		{
			if (waveForms.Length == 1) return waveForms[0];
			int width = waveForms[0].width;
			int height = 0;
			foreach (Texture2D texture2D in waveForms)
				height += texture2D.height;
			Texture2D texture2D1 = new Texture2D(width, height, TextureFormat.ARGB32, false); int num = 0;
			foreach (Texture2D texture2D2 in waveForms)
			{
				num += texture2D2.height;
				texture2D1.SetPixels(0, height - num, width, texture2D2.height, texture2D2.GetPixels()); UnityEngine.Object.DestroyImmediate((UnityEngine.Object) texture2D2);
			} texture2D1.Apply(); return texture2D1;
		}

		#endregion

		#region Editor Window

		public static EditorWindow GetWindowIfExists( Type windowType )
		{
			UnityEngine.Object[] objs = Resources.FindObjectsOfTypeAll( windowType );

			foreach( UnityEngine.Object o in objs )
			{
				if( o.GetType() == windowType )
					return (EditorWindow)o;
			}

			return null;
		}

//		public static EditorWindow GetAnimationWindow( bool showWindow )
//		{
//			System.Type t = typeof(EditorWindow).Assembly.GetType("UnityEditor.AnimationWindow");
//			if( showWindow )
//			{
//				return EditorWindow.GetWindow( t );
//			}
//
//			System.Reflection.MethodInfo getWindow = typeof(EditorWindow).GetMethod( "GetWindowDontShow", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static  );
//			System.Reflection.MethodInfo getWindowGeneric = getWindow.MakeGenericMethod( t );
//			return (EditorWindow)getWindowGeneric.Invoke(null, null);
//		}

		#endregion

		#region Upgrade Sequences

		public static void Upgrade( FSequence sequence )
		{
			const int fluxVersion = Flux.FUtility.FLUX_VERSION;
			if( sequence.Version == fluxVersion )
				return;
			
			Debug.LogFormat( "Upgrading sequence '{0}' from version '{1}' to '{2}'", sequence.name, sequence.Version, fluxVersion );
			
			// is it before 2.0.0 release?
			if( sequence.Version < 200 )
			{
				// set TimelineContainer as Content
				Transform timelineContainer = null;
				foreach( Transform t in sequence.transform )
				{
					if( t.name == "Timeline Container" )
					{
						timelineContainer = t;
						break;
					}
				}

				if( timelineContainer == null )
				{
					timelineContainer = new GameObject( "SequenceContent" ).transform;
					timelineContainer.hideFlags |= HideFlags.HideInHierarchy;
				}

				sequence.Content = timelineContainer;
				
				// create a container, and add it to the sequence
				
				FContainer container = FContainer.Create( FContainer.DEFAULT_COLOR );
				sequence.Add( container );
				
				FTimeline[] timelines = timelineContainer.GetComponentsInChildren<FTimeline>();
				foreach( FTimeline timeline in timelines )
				{
					container.Add( timeline );
					List<FTrack> tracks = timeline.Tracks;
					
					foreach( FTrack track in tracks )
					{
						if( track is FAnimationTrack )
						{
							FAnimationTrack animTrack = (FAnimationTrack)track;
							if( animTrack.AnimatorController != null )
							{
								FAnimationTrackInspector inspector = (FAnimationTrackInspector)FAnimationTrackInspector.CreateEditor( animTrack );
								AnimatorController controller = (AnimatorController)animTrack.AnimatorController;
								inspector.UpdateLayer( controller == null || controller.layers.Length == 0 ? null : controller.layers[0] );
								inspector.serializedObject.ApplyModifiedProperties();
								if( controller.layers.Length > 0 )
								{
									while( controller.layers[0].stateMachine.stateMachines.Length > 0 )
										controller.layers[0].stateMachine.RemoveStateMachine( controller.layers[0].stateMachine.stateMachines[controller.layers[0].stateMachine.stateMachines.Length-1].stateMachine );
									while( controller.layers[0].stateMachine.states.Length > 0 )
										controller.layers[0].stateMachine.RemoveState( controller.layers[0].stateMachine.states[controller.layers[0].stateMachine.states.Length-1].state );
								}
								Editor.DestroyImmediate( inspector );
								FAnimationTrackInspector.RebuildStateMachine( animTrack );
							}
						}

						track.CacheMode = track.RequiredCacheMode;
					}
					//					foreach( FTrack track in tracks )
					//					{
					//						track.CacheType = track.DefaultCacheType();
					//					}
				}
			}
			
			sequence.Version = fluxVersion;
		}

		#endregion
	}
}
