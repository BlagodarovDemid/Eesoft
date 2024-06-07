using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Eesoft_YP.Models;

namespace Eesoft_YP
{
    public partial class DealsList : Window
    {
        public DealsList()
        {
            InitializeComponent();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Wrap_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void DragWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void PersonList_Click(object sender, RoutedEventArgs e)
        {
            PersonList personList = new PersonList();
            this.Close();
            personList.Show();
        }
        private void EstateList_Click(object sender, RoutedEventArgs e)
        {
            EstateList estateList = new EstateList();
            this.Close();
            estateList.Show();
        }
        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = false;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Deals.Children.Clear();
            Supplies.Items.Clear();
            Demands.Items.Clear();
            this.Window_Loaded(sender, e);
            Supplies.SelectedItem = null;
            Demands.SelectedItem = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EesoftContext db = new EesoftContext();
            List<Supply> list = new List<Supply>();
            List<EstateDemand> list1 = new List<EstateDemand>();
            list = (from s in db.Supplies join d in db.Deals on s.Id equals d.SupplyId select s).ToList();
            list1 = (from s in db.EstateDemands join d in db.Deals on s.Id equals d.DemandId select s).ToList();
            using (db)
            {
                foreach (var a in db.Supplies)
                {
                    bool flag = false;
                    foreach (var b in list)
                    {
                        if (a == b)
                        {
                            flag = true;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (flag)
                    {
                        continue;
                    }
                    SupplyBlock supplyUI = new SupplyBlock();
                    supplyUI.ID.Text = a.Id.ToString();
                    supplyUI.AgentID.Text = a.AgentId.ToString();
                    supplyUI.ClientID.Text = a.ClientId.ToString();
                    supplyUI.EstateID.Text = a.EstateId.ToString();
                    supplyUI.Price.Text = a.Price.ToString();
                    supplyUI.Name = "a" + Supplies.Items.Count.ToString();
                    Supplies.Items.Add(supplyUI);
                }
                foreach (var a in db.EstateDemands)
                {
                    bool flag = false;
                    foreach (var b in list1)
                    {
                        if (a == b)
                        {
                            flag = true;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (flag)
                    {
                        continue;
                    }
                    DemandsBlock demandUI = new DemandsBlock();
                    demandUI.ID.Text = a.Id.ToString();
                    demandUI.AgentID.Text = a.AgentId.ToString();
                    demandUI.ClientID.Text = a.ClientId.ToString();
                    demandUI.Address.Text = a.Address;
                    demandUI.MinPrice.Text = a.MinPrice.ToString();
                    demandUI.MaxPrice.Text = a.MaxPrice.ToString();
                    if (a.EstateType == 1)
                    {
                        demandUI.Type.Text = "Квартира";
                        demandUI.MinArea.Text = a.MinArea.ToString();
                        demandUI.MaxArea.Text = a.MaxArea.ToString();
                        demandUI.MinFloors.Text = a.MinFloor.ToString();
                        demandUI.MaxFloors.Text = a.MaxFloor.ToString();
                        demandUI.MinRooms.Text = a.MinRooms.ToString();
                        demandUI.MaxRooms.Text = a.MaxRooms.ToString();
                    }
                    else if (a.EstateType == 2)
                    {
                        demandUI.Type.Text = "Дом";
                        demandUI.MinArea.Text = a.MinArea.ToString();
                        demandUI.MaxArea.Text = a.MaxArea.ToString();
                        demandUI.MinFloors.Text = a.MinFloor.ToString();
                        demandUI.MaxFloors.Text = a.MaxFloor.ToString();
                        demandUI.MinRooms.Text = "-";
                        demandUI.MaxRooms.Text = "-";
                    }
                    else if (a.EstateType == 3)
                    {
                        demandUI.Type.Text = "Земля";
                        demandUI.MinArea.Text = a.MinArea.ToString();
                        demandUI.MaxArea.Text = a.MaxArea.ToString();
                        demandUI.MinFloors.Text = "-";
                        demandUI.MaxFloors.Text = "-";
                        demandUI.MinRooms.Text = "-";
                        demandUI.MaxRooms.Text = "-";
                    }
                    demandUI.Name = "a" + Demands.Items.Count.ToString();
                    Demands.Items.Add(demandUI);
                }
                foreach (var a in db.Deals)
                {
                    DealsBlock deal = new DealsBlock();
                    deal.ID.Text = a.Id.ToString();
                    deal.SupplyID.Text = a.SupplyId.ToString();
                    deal.DemandID.Text = a.DemandId.ToString();
                    foreach (var c in list1)
                    {
                        if (c.Id == a.DemandId)
                        {
                            if (c.EstateType == 1)
                            {
                                foreach (var b in list)
                                {
                                    if (b.Id == a.SupplyId)
                                    {
                                        deal.Com.Text = (36000 + b.Price / 100).ToString();
                                        deal.Price.Text = ((double)b.Price / 100 * 3).ToString();
                                        deal.Share.Text = ((double)(36000 + b.Price / 100) / 100 * 45).ToString();
                                        break;
                                    }
                                }
                            }
                            if (c.EstateType == 2)
                            {
                                foreach (var b in list)
                                {
                                    if (b.Id == a.SupplyId)
                                    {
                                        deal.Com.Text = (30000 + b.Price / 100).ToString();
                                        deal.Price.Text = ((double)b.Price / 100 * 3).ToString();
                                        deal.Share.Text = ((double)(30000 + b.Price / 100) / 100 * 45).ToString();
                                        break;
                                    }
                                }
                            }
                            if (c.EstateType == 3)
                            {
                                foreach (var b in list)
                                {
                                    if (b.Id == a.SupplyId)
                                    {
                                        deal.Com.Text = (30000 + b.Price / 50).ToString();
                                        deal.Price.Text = ((double)b.Price / 100 * 3).ToString();
                                        deal.Share.Text = ((double)(30000 + b.Price / 50) / 100 * 45).ToString();
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    Deals.Children.Add(deal);
                }
            }
        }
        private void Adding_Click(object sender, RoutedEventArgs e)
        {
            if (Supplies.SelectedItem == null || Demands.SelectedItem == null)
            {
                if (Supplies.SelectedItem != null)
                {
                    Demands.BorderBrush = Brushes.Red;
                }
                popup1.IsOpen = true;
            }
            EesoftContext db = new EesoftContext();
            EstateDemand b = null;
            bool flag = false;
            using (db)
            {
                foreach (DemandsBlock demandUI in Demands.Items)
                {
                    if (demandUI.Name.Remove(0, 1) == Demands.SelectedIndex.ToString())
                    {
                        foreach (var a in db.EstateDemands)
                        {
                            if (a.Id == Convert.ToInt32(demandUI.ID.Text))
                            {
                                flag = true;
                                b = a;
                            }
                        }
                    }
                }

                foreach (SupplyBlock supply in Supplies.Items)
                {
                    if (supply.Name.Remove(0, 1) == Supplies.SelectedIndex.ToString())
                    {
                        foreach (var a in db.Supplies)
                        {
                            if (a.Id == Convert.ToInt32(supply.ID.Text))
                            {
                                if (flag)
                                {
                                    db.Deals.Add(new Models.Deal
                                    {
                                        DemandId = b.Id,
                                        SupplyId = a.Id,
                                    });
                                }
                            }
                        }
                    }
                }
                db.SaveChanges();
                Deals.Children.Clear();
                Supplies.Items.Clear();
                Demands.Items.Clear();
                this.Window_Loaded(sender, e);
                Supplies.SelectedItem = null;
                Demands.SelectedItem = null;
            }
        }
    }
}
