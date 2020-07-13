# Unity-TextLineBreakFormatter
A small Unity tool that allows line breaks for custom rules. i.e. avoiding custom characters in the beginning of a line.
簡單的 Unity 文字避頭尾工具。（功能尚未完備）

![alt text](https://github.com/arcsinxdx/Unity-TextLineBreakFormatter/blob/master/ReadmeImages/readme-1.jpg)

# 快速使用
1. 將 Prefab 物件拖曳至場景中（必須在 Canvas 物件底下）並更改文字物件的文字。
2. 在 "Avoid At Start Of Line" 定義避頭字元。已經事先定義幾個常見要避開的字元（例如句號「。」逗號「，」等等）

![alt text](https://github.com/arcsinxdx/Unity-TextLineBreakFormatter/blob/master/ReadmeImages/readme-3.png)

你也可以把 TextLineBreakFormatter.cs 加到場景上的物件，然後在 TextToReformat 指定文字物件。
目前只能避頭，無法偵測句尾，也無法偵測連續兩個以上符號的偵測（會被分開換行：例如六點刪節號以及全型句號 + 下引號的組合）。

# Quick-Start 
1. Simply drag the prefab to the scene (It has to be placed under a Canvas object). After that change the content of the Text.
2. Define the characters you want to avoid at the start of a line in "Avoid At Start Of Line". (Some characters are predefined from common characters from East Asian languages.)
3. Press Play button and the text will be formatted. Alternately, you can press "Format Text" in the context menu in Editor to format the text.

![alt text](https://github.com/arcsinxdx/Unity-TextLineBreakFormatter/blob/master/ReadmeImages/readme-2.png)

# How to Use from Add Component
1. Add the TextLineBreakFormatter.cs component to a game object.
2. Drag the Text component to the "Text To Reformat" slot in the inspector.
3. Define the characters you want to avoid at the start of a line. (Some characters are predefined from common characters from East Asian languages.)
4. Press Play button and the text will be formatted. Alternately, you can press "Format Text" in the context menu in Editor to format the text.

# Limitations
1. It will only check one character ahead, so it cannot detect continuous characters like "⋯⋯" （六點刪節號）or "。」"  （全型句號 + 下引號）
2. It only detects leading characters for now. (Ending characters not supported.)
