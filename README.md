# FishyGrind

FishyGrind is a tiny, focused World of Warcraft helper for anglers: a simple macro that searches your bags for a fishing lure (or any consumable/enchant item you specify) and applies it to your equipped fishing pole (the main hand weapon slot). The goal is to make reapplying lures painless and fast without digging through bags manually.

This project contains only the macro example and documentation for customizing and using it in the in-game macro system. It is intentionally lightweight and intended for manual use by players inside the WoW client.

What this project does
- Supplies a short, copy-pasteable macro that searches your container slots for an item whose link contains a configured name/substring and applies that item to an inventory slot (commonly the main hand).
- Documents how the macro works, how to customize the item to search for, and how to change the target inventory slot.
- Suggests minor robustifications (plain matching, case-insensitive matching, alternate slots) and usage notes.

Features
- Searches all standard bag indexes (backpack + bag slots) for a configured lure name.
- Uses the first matching item it finds and applies it to the chosen inventory slot (default: main hand, slot 16).
- Easy to customize for any lure, bait, or enchantable consumable by changing a single string or the target slot number.

Important notes and warnings
- This project is not a bot. It provides a single macro to assist with applying lures only. It does not automate fishing casts, movement, looting, or any other gameplay behavior.
- Macros that automate gameplay or perform actions without direct player input can be against Blizzard's Terms of Service. Use responsibly and within the rules of the game.
- Some protected UI functions can be restricted during combat or under Blizzard's secure execution model. If the macro appears not to work while you are in combat, try it out of combat.
- Item names are localized per client language. Use the localized item name or a reliable substring. If you share macros with users on other locales, tell them to change the item name or use an item-id based match.

Recommended macro (improved and corrected)
This compact, robust version is easy to edit. Replace the value of nameString with the substring of the item you want to match. It searches bag indexes 0..4 (backpack + up to 4 bag slots) and applies the found item to inventory slot 16 (main hand).

/run local nameString = "Shiny Bauble" for b=0,4 do for s=1,GetContainerNumSlots(b) do local link = GetContainerItemLink(b,s) if link and string.find(link, nameString, 1, true) then UseContainerItem(b,s) UseInventoryItem(16) return end end end

Compact alternate (simple form from older example)
If you prefer a very short one-liner without a named variable, here's the simple form (less explicit about plain matching):

/run for b=0,4 do for s=1,GetContainerNumSlots(b) do local link = GetContainerItemLink(b,s) if link and string.find(link, "Shiny Bauble") then UseContainerItem(b,s) UseInventoryItem(16) return end end end

How it works (walkthrough)
- The outer loop iterates bag indexes (0..4). Adjust if you have a different bag setup.
- The inner loop iterates every slot in each bag using GetContainerNumSlots(b).
- GetContainerItemLink(b,s) returns the item link for that slot or nil if empty.
- string.find(link, nameString, 1, true) looks for the exact substring (plain matching) inside the link. If found, the macro calls UseContainerItem(b,s) followed by UseInventoryItem(16) to apply the lure.
- The macro returns immediately after applying the first match it finds.

Customization tips
- Change the lure name: Replace "Shiny Bauble" with the item name (or a unique substring). For example: "Aquatic Lure".
- Case-insensitive matching: If you want case-insensitive matching, lowercase both the link and the search string. Example:

/run local nameString = "shiny bauble" for b=0,4 do for s=1,GetContainerNumSlots(b) do local link = GetContainerItemLink(b,s) if link and string.find(string.lower(link), nameString, 1, true) then UseContainerItem(b,s) UseInventoryItem(16) return end end end

- Apply to a different slot: Change the inventory slot number passed to UseInventoryItem. Common slots:
  - 16 = MainHandSlot
  - 17 = SecondaryHandSlot (off-hand)
  - 18 = Ranged slot (legacy; may be unused on some clients)

  If you want to apply to the off-hand, replace UseInventoryItem(16) with UseInventoryItem(17).
- Adjust bag range: If you use more or fewer bag indexes than the default 0..4, change the range used by the outer loop.

Alternate example (match by item ID substring in the link)
If you prefer to match an explicit item id embedded in the link, you can search for the id string. This is less dependent on localization but is slightly more brittle if item IDs change.

/run local idText = "item:12345" for b=0,4 do for s=1,GetContainerNumSlots(b) do local link = GetContainerItemLink(b,s) if link and string.find(link, idText, 1, true) then UseContainerItem(b,s) UseInventoryItem(16) return end end end

Notes on item IDs and localization
- Item link format contains the item id; searching for the id substring (e.g. "item:12345") is a common technique to avoid localization issues.
- If you share the macro publicly, tell other users to replace the search string with the localized name or the item id for their client.

Installation and usage
1. Open the in-game Macros window (/macro).
2. Create a new macro and paste the macro code above into the macro box.
3. Edit the search string and/or target inventory slot as desired.
4. Place the macro on your action bar and press it when you want to (re)apply the lure.

Troubleshooting
- Macro does nothing: Verify the search substring matches the item link text (or item id substring) exactly or uniquely.
- Lure doesn't apply: Ensure you have a pole or the intended target item equipped in the target slot, or change the inventory slot used by the macro.
- Wrong lure picked: The macro applies the first match it finds. Reorder items in your bags or use a more specific search string (or search by item id) to select the correct item.
- Combat/protected restrictions: If the macro fails in combat, retry out of combat. Some protected actions are restricted depending on the environment and client version.

Compatibility
- The macro uses basic WoW Lua API calls (GetContainerNumSlots, GetContainerItemLink, UseContainerItem, UseInventoryItem, string.find) that exist in many client versions. API names and behavior can differ between Classic and Retail clients or between expansions; if a function is missing or behaves differently on your client, adapt accordingly.

Contributing and suggestions
- This project is intentionally minimal. If you want to contribute alternate macro snippets, improved localization-friendly approaches, or compatibility notes for specific client versions, open an issue or send a patch.

License
This README and the example macro are provided without warranty. Use at your own risk. You may copy, adapt, and reuse the macro for your personal use.

Original macro (for reference)
The original snippet included several syntax errors and is shown only for historical reference. Use the corrected macros above.

/run for i=0,4 do for j=1,18 do local h=GetContainerItemLink if not(h(i,j)==nil)then if strfind(h(i,j), "Shiny Bauble")then p=PickupInventoryItem UseContainerItem (i,j) p(16)end ReplaceEnchant() end end end

Note: the macro provided earlier in this README is a corrected and more reliable form.