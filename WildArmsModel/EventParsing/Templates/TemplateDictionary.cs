using System.Collections.Generic;

namespace WildArmsModel.EventParsing.Templates
{
    public static class TemplateDictionary
    {
        public static Dictionary<int, ActionTemplate> Templates = new Dictionary<int, ActionTemplate>
        {
            { 0x00, new ActionTemplate(0x00, "Event Terminator", 0) },
            { 0x01, new ActionTemplate(0x01, "????", 2) },
            { 0x02, new ActionTemplate(0x02, "????", 0) },
            { 0x03, new ActionTemplate(0x03, "Dialog Start", 1) },
            { 0x04, new ActionTemplate(0x04, "????", 0) },
            { 0x05, new ActionTemplate(0x05, "????", 0) },
            { 0x06, new ActionTemplate(0x06, "Dialog Window", 1){ StringStart = true } },
            { 0x07, new ActionTemplate(0x07, "????", 0) },
            { 0x08, new ActionTemplate(0x08, "Memory Write", 0) { NullTerminated = true } },
            { 0x09, new ActionTemplate(0x09, "????", 0) },
            { 0x0A, new ActionTemplate(0x0A, "????", 0) },
            { 0x0B, new ActionTemplate(0x0B, "???? Crash", 0) },
            { 0x0C, new ActionTemplate(0x0C, "???? Crash", 0) },
            { 0x0D, new ActionTemplate(0x0D, "Change Area", 6) },
            { 0x0E, new ActionTemplate(0x0E, "????", 2) },
            { 0x0F, new ActionTemplate(0x0F, "????", 2) },
            { 0x10, new ActionTemplate(0x10, "????", 2) },
            { 0x11, new ActionTemplate(0x11, "???? (could be logic)", 2) },
            { 0x12, new ActionTemplate(0x12, "???? (stops movement)", 5) },
            { 0x13, new ActionTemplate(0x13, "???? (continuous?)", 0) },
            { 0x14, new ActionTemplate(0x14, "fade to black", 1) },
            { 0x15, new ActionTemplate(0x15, "???? Crash", 0) },
            { 0x16, new ActionTemplate(0x16, "???? Crash", 0) },
            { 0x17, new ActionTemplate(0x17, "????", 1) },
            { 0x18, new ActionTemplate(0x18, "???? (jump?)", 4) },
            { 0x19, new ActionTemplate(0x19, "Turn Invisible", 3) },
            { 0x1A, new ActionTemplate(0x1A, "???? (continuous?)", 0) },
            { 0x1B, new ActionTemplate(0x1B, "???? (terminator?)", 0) },
            { 0x1C, new ActionTemplate(0x1C, "????", 3) },
            { 0x1D, new ActionTemplate(0x1D, "Turn opaque", 3) },
            { 0x1E, new ActionTemplate(0x1E, "????", 6) },
            { 0x1F, new ActionTemplate(0x1F, "???? (conditional?)", 3) },
            { 0x20, new ActionTemplate(0x20, "Start Timer", 10) },
            { 0x21, new ActionTemplate(0x21, "????", 3) },
            { 0x22, new ActionTemplate(0x22, "????", 3) },
            { 0x23, new ActionTemplate(0x23, "????", 8) },
            { 0x24, new ActionTemplate(0x24, "Call Credits", 0) },
            { 0x25, new ActionTemplate(0x25, "????", 5) },
            { 0x26, new ActionTemplate(0x26, "Stop Momentum", 0) },
            { 0x27, new ActionTemplate(0x27, "File Select (more arguments?)", 0) },
            { 0x28, new ActionTemplate(0x28, "????", 2) }
        };
    }
}
