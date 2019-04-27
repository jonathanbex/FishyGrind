# FishyGrind
Fishy lishy

# Macro for Lure
/run for i=0,4 do for j=1,18 do local h=GetContainerItemLink if not(h(i,j)==nil)then if strfind(h(i,j), "Shiny Bauble")then p=PickupInventoryItem UseContainerItem (i,j) p(16)end ReplaceEnchant() end end end

edit Shiny Bauble to whatever lure you want to use
