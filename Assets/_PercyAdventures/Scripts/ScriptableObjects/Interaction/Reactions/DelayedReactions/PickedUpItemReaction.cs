public class PickedUpItemReaction : DelayedReaction
{
    public Item item;               // The item asset to be added to the Inventory.


    private InventoryManager inventory;    // Reference to the Inventory component.


    protected override void SpecificInit()
    {
        inventory = InventoryManager.instance;
    }


    protected override void ImmediateReaction()
    {
        inventory.AddItem(item);
    }
}
