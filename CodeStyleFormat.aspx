<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodeStyleFormat.aspx.cs"
    Inherits="TestApplication.CodeStyleFormat" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<!-- THIS STUFF GOES IN THE HEADER TEMPLATE -->
<style type="text/css">
    pre.formatted-source-code
    {
        font-family: Andale Mono, Lucida Console, Monaco, fixed, monospace;
        color: #000000;
        background-color: #eee;
        font-size: 12px;
        border: 1px dashed #999999;
        line-height: 14px;
        padding: 5px;
        overflow: auto;
        width: 100%;
    }
    p.warning
    {
        color: #000000;
        background-color: #FFB6C1;
        font-size: 12px;
        border: 3px double #333333;
        line-height: 14px;
        padding: 5px;
        overflow: auto;
        width: 100%;
    }
    #txtRawSourceCode
    {
        height: 150px;
        width: 575px;
    }
    #txtFormattedSourceCode
    {
        height: 200px;
        width: 576px;
    }
    input.groovybutton
    {
        font-size: 15px;
        font-family: Lucida Console,monospace;
        font-weight: bold;
        height: 50px;
        background-color: #DDDDDD;
        border-top-style: groove;
        border-bottom-style: double;
        border-left-style: ridge;
        border-right-style: outset;
    }
</style>
<script type="text/javascript"> 
<!--
    var Color = new Array();
    Color[1] = "ff";
    Color[2] = "ee";
    Color[3] = "dd";
    Color[4] = "cc";
    Color[5] = "bb";
    Color[6] = "aa";
    Color[7] = "99";

    function fadeIn(where) {
        if (where >= 1) {
            document.getElementById('fade').style.backgroundColor = "#ffff" + Color[where];
            if (where > 1) {
                where -= 1;
                setTimeout("fadeIn(" + where + ")", 200);
            } else {
                where -= 1;
                setTimeout("fadeIn(" + where + ")", 200);
                document.getElementById('fade').style.backgroundColor = "transparent";
            }
        }
    }

    function formatSourceCode() {
        var strIn = document.getElementById("txtRawSourceCode").value;
        var strOut = null;
        //        if (document.getElementById("embedstyle").checked) {
        strOut = "<pre style=\"font-family: Andale Mono, Lucida Console, Monaco, fixed, monospace; color: #000000; background-color: #eee;font-size: 12px;border: 1px dashed #999999;line-height: 14px;padding: 5px; overflow: auto; width: 100%\"><code>";
        //            hideElement("style");
        //        } else {
        //            strOut = "<pre class=\"source-code\"><code>";
        //            showElement("style");
        //        }
        var strOut25 = null;
        var line = 1;
        var strTab;
        var hasVerticalPipe = false;
        var j;

        if (document.getElementById("rbTab4").checked) {
            strTab = "    ";
        } else if (document.getElementById("rbTab2").checked) {
            strTab = "  ";
        } else {
            strTab = "        ";
        }

        for (i = 0; i < strIn.length; i++) {
            var code = strIn.charCodeAt(i);
            switch (code) {
                case 9: // tab
                    strOut += strTab;
                    break;
                case 10:  // line-feed
                case 13:
                    strOut += "\n";
                    line += 1;
                    //                    if (line == 26) {
                    //                        strOut25 = strOut + "[only the first 25 lines shown in this example]\n\n";
                    //                    }
                    j = i + 1;
                    if (code == 13 && j < strIn.length && strIn.charCodeAt(j) == 10) {
                        i++;
                    }
                    break;
                case 34: // Quotation "
                    strOut += "</span><span style=\"background-color: transparent; color: blue; font-family: 'Courier New'; font-size: 11px; font-weight: normal;\">&quot;";
                    break;
                case 38:
                    strOut += "<span style=\"background-color: transparent; color: red; font-family: 'Courier New'; font-size: 11px; font-weight: normal;\">&amp;";
                    break;
                case 59: // Semi Colon
                    strOut += ";</span>";
                    break;
                case 60: // Les than <
                    strOut += "</span><span style=\"background-color: transparent; color: maroon; font-family: 'Courier New'; font-size: 11px; font-weight: normal;\">&lt;";
                    break;
                case 62: // Greater than >
                    strOut += "&gt;</span></span><span style=\"color: black;\">";
                    break;
                case 124: // vertical pipe (blogger modifies this)
                    strOut += "&#124;";
                    hasVerticalPipe = true;
                    break;
                case 32: // Blank Space
                    var previousCode = strIn.charCodeAt(i - 1);
                    switch (previousCode) {
                        case 34: // Quotation "
                            strOut += " </span><span style=\"background-color: transparent; color: red; font-family: 'Courier New'; font-size: 11px; font-weight: normal;\">";
                            break;
                        default:
                            strOut += " ";
                            break;
                    }
                    break;
                case 61: // Equals Sign "="
                    strOut += "<span style=\"background-color: transparent; color: blue; font-family: 'Courier New'; font-size: 11px; font-weight: normal;\">=";
                    break;
                case 47: //Slash (forward or divide)
                    var nextCode = strIn.charCodeAt(i + 1);
                    switch (nextCode) {
                        case 62: // Greater than >
                            strOut += "</span><span style=\"background-color: transparent; color: maroon; font-family: 'Courier New'; font-size: 11px; font-weight: normal;\">/";
                            break;
                        default:
                            strOut += "/";
                            break;
                    }
                    break;
                default:
                    if (code > 32 && code <= 127) {
                        strOut += strIn.charAt(i);
                    } else {
                        strOut += "&#" + code + ";";
                    }
                    break;
            } // switch
        } // for
        strOut += "\n</code></pre>";
        var textoutelement = document.getElementById("txtFormattedSourceCode");
        textoutelement.value = strOut;
        textoutelement.focus();
        textoutelement.select();

        if (hasVerticalPipe) {
            showElement("div-vertical-pipe-warning");
        } else {
            hideElement("div-vertical-pipe-warning");
        }

        var resultselement = document.getElementById("results");
        if (strOut25 != null) {
            resultselement.innerHTML = strOut25;
        } else {
            resultselement.innerHTML = strOut;
        }

        fadeIn(7);
    }

    function onloadEvent() {
        var textinelement = document.getElementById("txtRawSourceCode");
        textinelement.focus();
        textinelement.select();
    }

    function showElement(strId) {
        var ref = document.getElementById(strId);
        if (ref.style) { ref = ref.style; }
        ref.display = '';
    }

    function hideElement(strId) {
        var ref = document.getElementById(strId);
        if (ref.style) { ref = ref.style; }
        ref.display = 'none';
    }

//-->

</script>
<body>
    <form id="form1" runat="server">
    <div id="style" style="display: none;">
        Example Stylesheet:
        <textarea cols="50" id="Textarea1" rows="13" wrap="off">&lt;style type="text/css"&gt;                                                                     pre.formatted-source-code                                                                     {                                                                         font-family: Andale Mono, Lucida Console, Monaco, fixed, monospace;                                                                         color: #000000;                                                                         background-color: #eee;                                                                         font-size: 12px;                                                                         border: 1px dashed #999999;                                                                         line-height: 14px;                                                                         padding: 5px;                                                                         overflow: auto;                                                                         width: 100%;                                                                     }                                                                 &lt;/style&gt;</textarea></div>
    <div>
        <div>
            So today here I am, coming up with a <strong>Source Code Formatting Tool for Blogging</strong>.
            Previously I had a post regarding <a href="http://www.codeshode.com/2010/06/format-my-source-code-for-blogging.html"
                target="_blank">How To Format My Source Code for Blogging?</a> which explained
            the use of <a href="http://alexgorbatchev.com/wiki/SyntaxHighlighter" target="_blank">
                SyntaxHighlighter</a> to format your source code for blogging. But now I feel
            it kinda slow down the blog to load a post. So I decided to create a tool that will
            provide me with Formatted HTML Code of my Source Code to post in a blog. There will
            be a series of such tools e.g.
            <ul>
                <li>Format <strong>HTML</strong> Source Code for Blogging </li>
                <li>Format <strong>XML</strong> Source Code for Blogging</li>
                <li>Format <strong>ASPX</strong> Source Code for Blogging </li>
                <li>Format <strong>Javascript</strong> Source Code for Blogging </li>
                <li>Format <strong>SQL</strong> Source Code for Blogging </li>
                <li>Format <strong>C#</strong> Source Code for Blogging </li>
                <li>Format <strong>VB.NET</strong> Source Code for Blogging </li>
                <li>Format <strong>CSS</strong> Source Code for Blogging </li>
            </ul>
            and much more. Right now, I have this tool, which will format your HTML/XML/ASPX
            source sode for blogging. What you need to do is Paste your Source Code in the given
            Text Area and your formatted source code will be provided in the other Text Area.
            Just copy it and paste it in your blog post or where ever you want. And thats that!
            <strong>P.S.</strong> Other Source&nbsp;Code Formatting Tool will be coming shortly
            or they will get embedded in current tool may. Happy Coding! Happy Shoding!
        </div>
        <div style="text-align: center;">
            <b>::HTML-XML-ASPX Source Code Formatting Tool::</b></div>
        <div style="text-align: center;">
        </div>
        <textarea id="txtRawSourceCode" wrap="off">Paste your text here.</textarea>
        <table>
            <tbody>
                <tr>
                    <td>
                            <input type="button" name="groovybtn1" class="groovybutton" value="Click Here to Format Source Code"
                                title="Click Here to Format Source Code" onclick="javascript:formatSourceCode();" />
                    </td>
                    <td>
                        Tab size:
                        <input id="rbTab2" name="tabsize" type="radio" />2<input checked="true" id="rbTab4"
                            name="tabsize" type="radio" />4<input id="rbTab8" name="tabsize" type="radio" />8
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="step-instr" id="fade">
            Copy the HTML below to your clipboard.
            <textarea id="txtFormattedSourceCode" wrap="off">formatted HTML will appear in here.</textarea></div>
        <div id="div-vertical-pipe-warning" style="display: none;">
            <div class="warning">
                <b>Vertival Pipe Character Warning:</b> The text contains the vertical pipe character
                '|' which Blogger's editor may remove. Blogger's editor on the web has two edit
                tabs: "Edit HTML" and "Compose". The "Compose" tab will remove all | characters!
                Use the "Edit HTML" tab only.</div>
        </div>
        <div id="results">
            <pre class="formatted-source-code"><code>This is an example of what your text will look
                like.</code></pre>
            <pre class="formatted-source-code"><code>• Tabs are converted to spaces. </code><span
                class="Apple-style-span" style="font-family: monospace;">• Quotes and other special
                characters are converted to HTML. </span><code>• Everything is enclose in HTML's 'pre'
                    and 'code' tags. • Style is set. • Fixed width font. • Shaded box. • Dotted line
                    border. </code></pre>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
