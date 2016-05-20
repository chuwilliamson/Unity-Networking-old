using UnityEngine;
using UnityEditor;

namespace Editor
{
    /// <summary>
    /// Stores some configuration values to be used across all of my 'Animation_XX_Editor's
    /// </summary>
    public static class Globals
    {
        public static readonly float BUTTON_WIDTH = EditorGUIUtility.singleLineHeight + 8f;

        public static readonly float DEFAULT_HEIGHT = EditorGUIUtility.singleLineHeight;

        public static readonly float SPACING_WIDTH = 5f;
        public static readonly float SPACING_HEIGHT = EditorGUIUtility.singleLineHeight + 5;

        public const float FOLDOUT_WIDTH = 50;
    }

    [CustomPropertyDrawer(typeof(AnimationSequence))]
    public class AnimationSequenceEditor : PropertyDrawer
    {
        /// <summary>
        /// This is how you draw to the inspector. You aren't really limited to any specific thing 
        /// as images can be drawn. The only limit is how terrible is it to try to place everything
        /// just so. Not to mention you still have to figure out the height of the stuff you just 
        /// drew. Probably the worst API I've seen from Unity.
        /// </summary>
        /// <param name="a_Position"> The position this property should start drawing to </param>
        /// <param name="a_Property"> The property to be drawn </param>
        /// <param name="a_Label"> The name of the property. Seems useless </param>
        public override void OnGUI(Rect a_Position, SerializedProperty a_Property, GUIContent a_Label)
        {
            // Store the value of the original indent to be restored later
            int originalIndent = EditorGUI.indentLevel;

            // Let Unity know you are starting this property
            EditorGUI.BeginProperty(a_Position, a_Label, a_Property);
            {
                // Setup the Rect 'foldoutRect' with the current value of 'a_Position'
                Rect foldoutRect = a_Position;

                // Set the size of the foldout, create it, and store its return value into this property
                foldoutRect.size = new Vector2(Globals.FOLDOUT_WIDTH, Globals.DEFAULT_HEIGHT);
                a_Property.isExpanded = EditorGUI.Foldout(
                    foldoutRect, a_Property.isExpanded, a_Property.displayName);

                // if the foldout is expanded by the user
                if (a_Property.isExpanded)
                {
                    // Move to the 'AnimationLayer' array: 'animationLayers'
                    a_Property.Next(true);
                    a_Property.Next(true);

                    // Sets the correct amount of space after a label even after inspector resizing
                    // It's empty because we want to use new this position without displaying information
                    Rect buttonRect = EditorGUI.PrefixLabel(a_Position, new GUIContent(" "));

                    // Set the size of and create button that will increase the size of the 'animationLayers' array
                    buttonRect.size = new Vector2(Globals.BUTTON_WIDTH, Globals.DEFAULT_HEIGHT);
                    if (GUI.Button(buttonRect, new GUIContent("+", "Add Animation Layer")))
                        a_Property.arraySize++;

                    // Set the position of and create button that will
                    // decrease the size of the 'animationLayers' array
                    buttonRect.x += buttonRect.width + Globals.SPACING_WIDTH;
                    if (GUI.Button(buttonRect, new GUIContent("-", "Remove Animation Layer")))
                        a_Property.arraySize--;

                    // Back to using the original 'position' variable
                    // Move down below the buttons that were just placed
                    a_Position.y += Globals.SPACING_HEIGHT;

                    EditorGUI.indentLevel++;
                    for (int i = 0; i < a_Property.arraySize; ++i)
                    {
                        // Draw the 'AnimationLayer' according to my custom drawer
                        EditorGUI.PropertyField(a_Position, a_Property.GetArrayElementAtIndex(i));

                        // Let the child property do all the hard work to calculate the next y value
                        // In this case, it calls 'AnimationLayerEditor.GetPropertyHeight' and passes this property
                        a_Position.y += EditorGUI.GetPropertyHeight(a_Property.GetArrayElementAtIndex(i));
                    }
                }
            }
            // Let Unity know you are finished with the property
            EditorGUI.EndProperty();

            // Restore the original indent value
            EditorGUI.indentLevel = originalIndent;
        }
        /// <summary>
        /// This is where you will need to parse the property height. It's not as easy as it sounds
        /// when you have resizing elements based on conditions and other sizes. This is because this 
        /// object cannot store any member variables reliably even though it's not a static object.
        /// </summary>
        /// <param name="a_Property"> The property that needs it's height parsed from </param>
        /// <param name="a_Label"> The name of the property. Seems useless </param>
        /// <returns> The total height of the property </returns>
        public override float GetPropertyHeight(SerializedProperty a_Property, GUIContent a_Label)
        {
            // Sets up a variable to add space to our property
            float extraSpace = 0;   

            // if the foldout is expanded by the user
            if (a_Property.isExpanded)
            {
                // Move to the 'AnimationLayer' array: 'animationLayers'
                a_Property.Next(true);
                for (int i = 0; i < a_Property.arraySize; ++i)
                {
                    // Let the child property do all the hard work to calculate its height
                    // In this case, it calls 'AnimationLayerEditor.GetPropertyHeight' and gives it this property
                    extraSpace += EditorGUI.GetPropertyHeight(a_Property.GetArrayElementAtIndex(i));
                }
            }
            // return the base height plus our 'extraSpace'
            return base.GetPropertyHeight(a_Property, a_Label) + extraSpace;
        }
    }
}