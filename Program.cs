using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
public class Program
{
    public static void Main(string[] args)
    {
        InventoryManagement inventoryManagement = new InventoryManagement();
        UserInterface userInterface = new UserInterface(inventoryManagement);
        userInterface.Start();
    }
}

}
