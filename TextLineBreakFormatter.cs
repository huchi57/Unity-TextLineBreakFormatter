using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLineBreakFormatter : MonoBehaviour{

    public Text textToReformat;
    public List<char> avoidAtStartOfLine = new List<char> {'，', ',', '。', '！', '？', '：', ';', '」', '』', '］', '｝', '＞', '》', '〉', '－', '⋯'};

    [Header("Debug (Set by script in Play Mode)")]

    //[Header("Leading Characters")]
    [SerializeField] List<char> leadingCharacters;
    [SerializeField] List<int> leadingCharactersIndex;

    //[Header("Ending Characters (TODO)")]
    [SerializeField] List<char> endingCharacters;
    [SerializeField] List<int> endingCharactersIndex;

    TextGenerationSettings settings;
    TextGenerator generator;
    string fullContent;
    //bool textFullyFormatted;

    private void Start() {
        FormatText();
    }

    // This is set to public so it can be called from the outside
    // For example when the text content changes we may want to reformat again
    [ContextMenu("Format Text")]
    public void FormatText(){

        if(textToReformat == null){
            Debug.LogWarning("Cannot find text to reformat.");
            return;
        }

        // TODO: Mark text not fully formatted
        // textFullyFormatted = false;

        // Copy original text first
        fullContent = textToReformat.text;

        //////////////////////////////
        // Step 1: new settings     //
        //////////////////////////////

        // GetGenerationSettings needs a [Vector2 extents] parameter
        // We get the extents by getting the RectTransform of the Text
        settings = CopyFrom(textToReformat.GetGenerationSettings(textToReformat.rectTransform.rect.size));

        // Print the settings out
        // PrintTextGenerationSettings(settings);

        //////////////////////////////
        // Step 2: new generator    //
        //////////////////////////////

        // Create a new TextGenerator
        generator = new TextGenerator();

        // Populate it with content and settings from step 1
        generator.Populate(fullContent, settings);

        // Print the text generator out
        // PrintTextGenerator(generator);

        // TODO: Keep reformatting until the text is fully formatted
        //while(textFullyFormatted == false){

            // Get the leading characters of each line
            GetLeadingAndEndingCharacters(generator.lines);

            // Format the text
            GenerateFormattedText();
        //}
    }

    // Get each line's leading and endinc characters
    void GetLeadingAndEndingCharacters(IList<UILineInfo> list) {

        // Initialize the lists
        leadingCharacters = new List<char>();
        leadingCharactersIndex = new List<int>();
        endingCharacters = new List<char>();
        endingCharactersIndex = new List<int>();

        // Traverse the whole list
        for (int i = 0; i < list.Count; i++){

            // fullContent: the original text
            // list[i]: line info
            // startCharIdx: the (fullContent[list[i]].startCharIdx)th character of the fullContent is the first character in the (i)th line
            leadingCharactersIndex.Add(list[i].startCharIdx);
            leadingCharacters.Add(fullContent[list[i].startCharIdx]);

            // Prevent overflow
            if (i < list.Count - 1){
                endingCharactersIndex.Add(list[i + 1].startCharIdx - 1);
                endingCharacters.Add(fullContent[list[i + 1].startCharIdx - 1]);
            }
        }
    }

    // Generate formatted text
    void GenerateFormattedText(){

        // TODO: Ending characters detection

        // Each time we add a new line (\n), we add offset by 1 (since we insert a new character)
        int offset = 0;
        int indexToInsert = 0;

        // TODO: We set textFullyFormatted to true here first.
        // If any changes happen to the text, we set it to false
        // textFullyFormatted = true;

        // Check for each line:
        for (int i = 0; i < leadingCharacters.Count; i++){

            // If the character is not allowed at the start of the line:
            if(avoidAtStartOfLine.Contains(leadingCharacters[i])){

                // TODO: 0. Set text not formatted
                // textFullyFormatted = false;

                // 1. Get the position we want to insert the new character
                indexToInsert = leadingCharactersIndex[i] + offset - 1;

                // 2. Insert a new line symbol (\n)
                fullContent = fullContent.Insert(indexToInsert, "\n");

                // 3. Add offset by one
                offset++;

                //print(fullContent);
            }
        }

        // Display the formatted text
        textToReformat.text = fullContent;
    }


    // Copy a TextGenerationSettings from an existed one
    TextGenerationSettings CopyFrom(TextGenerationSettings o) {
        return new TextGenerationSettings {
            font = o.font,
            color = o.color,
            fontSize = o.fontSize,
            lineSpacing = o.lineSpacing,
            richText = o.richText,
            scaleFactor = o.scaleFactor,
            fontStyle = o.fontStyle,
            textAnchor = o.textAnchor,
            alignByGeometry = o.alignByGeometry,
            resizeTextForBestFit = o.resizeTextForBestFit,
            resizeTextMinSize = o.resizeTextMinSize,
            resizeTextMaxSize = o.resizeTextMaxSize,
            updateBounds = o.updateBounds,
            verticalOverflow = o.verticalOverflow,
            horizontalOverflow = o.horizontalOverflow,
            generationExtents = o.generationExtents,
            pivot = o.pivot,
            generateOutOfBounds = o.generateOutOfBounds
        };
    }

    // Printing info of a TextGenerator
    void PrintTextGenerator(TextGenerator g){
        print("<color=yellow>===PRINTING TEXT GENERATOR INFO===</color>");
        print("Character Count: " + g.characterCount);
        print("Character count visible: " + g.characterCountVisible);
        print("Characters: A list with the count of " + g.characters.Count);
        print("Font size used for best fit: " + g.fontSizeUsedForBestFit);
        print("Line count: " + g.lineCount);
        print("Lines: A list with the count of " + g.lines);
        print("Rect extents: " + g.rectExtents);
        print("Vertex count: " + g.vertexCount);
        print("Verts: " + g.verts);
    }

    // Printing info of a TextGenerationSettings
    void PrintTextGenerationSettings(TextGenerationSettings s){
        print("<color=yellow>===PRINTING TEXT GENERATOR SETTINGS INFO===</color>");
        print("Font: " + s.font);
        print("Color: " + s.color);
        print("Font size: " + s.fontSize);
        print("Line spacing: " + s.lineSpacing);
        print("Rich text: " + s.richText);
        print("Scale factor: " + s.scaleFactor);
        print("Font style: " + s.fontStyle);
        print("Text anchor: " + s.textAnchor);
        print("Align by geometry: " + s.alignByGeometry);
        print("Resize for best fit: " + s.resizeTextForBestFit);
        print("Resize min size: " + s.resizeTextMinSize);
        print("Resize max size: " + s.resizeTextMaxSize);
        print("Update bounds: " + s.updateBounds);
        print("Vertical overflow: " + s.verticalOverflow);
        print("Horizontal overflow: " + s.horizontalOverflow);
        print("Generation extents: " + s.generationExtents);
        print("Pivot: " + s.pivot);
        print("Generate out of bounds: " + s.generateOutOfBounds);
    }

}
