using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Beatmaps;

public static class OsuBeatmapParser
{
    /// <summary>
    /// Parse .osu text and produce Beatmap.
    /// Hit times and durations are in seconds.
    /// Note color for hit circles is derived from hitSound bits:
    /// whistle (2) or clap (8) -> Blue (Kat), otherwise Red (Don).
    /// </summary>
    public static Beatmap Parse(string osuText)
    {
        var lines = osuText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        var mapElements = new List<MapElement>();
        bool hitObjectsSection = false;
        double sliderMultiplier = 1.0;

        foreach (var rawLine in lines)
        {
            var line = rawLine.Trim();
            if (line.Length == 0) continue;

            if (line.StartsWith("SliderMultiplier:", StringComparison.OrdinalIgnoreCase))
            {
                var parts = line.Split(':');
                if (parts.Length == 2 && double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var sm))
                    sliderMultiplier = sm;
            }

            if (line.StartsWith("[HitObjects]", StringComparison.OrdinalIgnoreCase))
            {
                hitObjectsSection = true;
                continue;
            }

            if (!hitObjectsSection) continue;

            var partsHO = line.Split(',');
            if (partsHO.Length < 5) continue; // must have at least x,y,time,type,hitSound

            // time in ms -> sec
            if (!float.TryParse(partsHO[2], NumberStyles.Float, CultureInfo.InvariantCulture, out var hitTimeMs))
                continue;
            float hitTimeSec = hitTimeMs / 1000f;

            if (!int.TryParse(partsHO[3], out var type)) type = 0;
            if (!int.TryParse(partsHO[4], out var hitSound)) hitSound = 0;

            // Determine color for hit circles based on hitSound bits:
            // whistle (bit 1 -> value 2) or clap (bit 3 -> value 8) -> Kat (Blue)
            // otherwise Don (Red).
            Notes noteColor = ((hitSound & 2) != 0 || (hitSound & 8) != 0) ? Notes.Blue : Notes.Red;

            // Hit circle -> SingleNote
            if ((type & 1) != 0)
            {
                mapElements.Add(new SingleNote(hitTimeSec, noteColor));
                continue;
            }

            // Slider -> Drumroll (keep previous duration heuristic; you can improve using TimingPoints)
            if ((type & 2) != 0)
            {
                // partsHO format: x,y,time,type,hitSound,curveType|points,slides,length,edgeSounds,edgeSets,hitSample
                // partsHO[6] = slides (repeat count), partsHO[7] = length (pixels)
                float durationSec = 0f;
                if (partsHO.Length > 7 &&
                    float.TryParse(partsHO[7], NumberStyles.Float, CultureInfo.InvariantCulture, out var pxLength))
                {
                    int repeats = 1;
                    if (partsHO.Length > 6) int.TryParse(partsHO[6], out repeats);
                    // simple heuristic: pxLength * repeats / sliderMultiplier gives milliseconds-like value in our previous code,
                    // convert to seconds.
                    durationSec = (float) (pxLength * repeats / Math.Max(1e-6, sliderMultiplier)) / 1000f;
                }
                else
                {
                    // fallback small drumroll if length missing
                    durationSec = 0.25f;
                }

                mapElements.Add(new Drumroll(hitTimeSec, durationSec));
                continue;
            }

            // Spinner -> Spinner
            if ((type & 8) != 0)
            {
                if (partsHO.Length > 5 && float.TryParse(partsHO[5], NumberStyles.Float, CultureInfo.InvariantCulture, out var endTimeMs))
                {
                    float durationSec = (endTimeMs - hitTimeMs) / 1000f;
                    mapElements.Add(new Spinner(hitTimeSec, durationSec));
                }
                continue;
            }
        }

        // TODO: compute real BPM & Difficulty from TimingPoints/Difficulty if needed.
        return new Beatmap(120, mapElements, null);
    }
}
