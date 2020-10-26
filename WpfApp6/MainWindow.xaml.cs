using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using ConsoleApp1;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ageRule;
        
        public MainWindow()
        {
            InitializeComponent();
            File.Delete("dataFile.xml");
            for (int i = 1; i <= 6; i++)
            {
                NumOfMockInput.Items.Add(i);
            }
            AdditionalQuestionInput.Visibility = Visibility.Hidden;
            AdditionalQuestionInput.Items.Add("Yes");
            AdditionalQuestionInput.Items.Add("No");
            GenderInput.Items.Add("Male");
            GenderInput.Items.Add("Female");
            GenderInput.Items.Add("others");
            SearchKey.Items.Add("First Name");
            SearchKey.Items.Add("Last Name");
            SearchKey.Items.Add("Age");
            SearchKey.Items.Add("Gender");
            SearchKey.Items.Add("Module");
            SearchInput.IsEnabled = false;
            SlotInput.IsEnabled = false;
            SaveButton.IsEnabled = false;
            ShowButton.IsEnabled = false;
            string[] names = Enum.GetNames(typeof(ModuleSelection));
            foreach (string name in names)
            {
                ModuleInput.Items.Add(name);
            }
            DataContext = this;
        }
        ModuleSelection moduleSelection;
        Appointment[] appArray;
        AppointmentList appointmentList = new AppointmentList();
            public string AgeRule { get => ageRule; set => ageRule = value; }
        public bool ValidateInput(string input)
        {
            Regex reg = new Regex(@"[~`!@#$%^&*()-+=|\{}':;.,<>/?]");
            Regex reg2 = new Regex("^[a-zA-Z ]");
            if (reg.IsMatch((string)input))
            {
                return false;
            }
            else if (!reg2.IsMatch((string)input))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            SlotInput.IsEnabled = true;
            SaveButton.IsEnabled = true;
            int check = 0;
            if (SlotInput.SelectedIndex == -1)
            {
                MessageBox.Show("Select Slot Time!");
                check = 1;
            }
            else
            {
                int slot = SlotInput.SelectedIndex + 1;
                if (appArray[slot - 1].MyData == null)
                {
                    string firstName = FirstNameInput.Text;
                    string lastName = LastNameInput.Text;
                    string phoneNum = PhoneNumberInput.Text;
                    string ageString = AgeInput.Text;
                    string creditCardNum = CreditCardInput.Text;

                    if (firstName.Length == 0)
                    {
                        MessageBox.Show("Enter First Name!");                       
                    }
                    else
                    {
                        if (lastName.Length == 0)
                        {
                            MessageBox.Show("Enter Last Name!");
                        }
                        else
                        {
                            if (ageString.Length == 0)
                            {
                                MessageBox.Show("Enter Age!");
                            }
                            else
                            {
                                if (phoneNum.Length == 0)
                                {
                                    MessageBox.Show("Enter Phone Number!");
                                }
                                else
                                {
                                    if (creditCardNum.Length == 0)
                                    {
                                        MessageBox.Show("Enter Credit Card Number!");
                                    }
                                    else
                                    {
                                        if (GenderInput.SelectedIndex == -1)
                                        {
                                            MessageBox.Show("Select Gender!");
                                            check = 1;
                                        }
                                        else
                                        {
                                            if (ModuleInput.SelectedIndex == -1)
                                            {
                                                MessageBox.Show("Select Module!");
                                                check = 1;
                                            }
                                            else
                                            {
                                                if (NumOfMockInput.SelectedIndex == -1)
                                                {
                                                    MessageBox.Show("Select Number of appointments!");
                                                    check = 1;
                                                }
                                                else
                                                {
                                                    if (SlotInput.SelectedIndex == -1)
                                                    {
                                                        MessageBox.Show("Select Slot!");
                                                        check = 1;
                                                    }
                                                    else
                                                    {
                                                        if (AdditionalQuestionInput.SelectedIndex == -1)
                                                        {
                                                            MessageBox.Show("Select preparation kit!");
                                                            check = 1;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    bool firstNameResult = ValidateInput(firstName);
                    if (firstNameResult == false)
                    {
                        FirstNameInput.Foreground = Brushes.Red;
                        check = 1;
                    }

                    bool lastNameResult = ValidateInput(lastName);
                    if (lastNameResult == false)
                    {
                        LastNameInput.Foreground = Brushes.Red;
                        check = 1;
                    }

                    long phoneNumber = 0;
                    if (!(long.TryParse(phoneNum, out phoneNumber) && PhoneNumberInput.Text.Length == 10) || phoneNumber < 0)
                    {
                        PhoneNumberInput.Foreground = Brushes.Red;
                        check = 1;
                    }

                    int age = 0;
                    if (!(int.TryParse(ageString, out age)) || age <= 0 || age > 120)
                    {
                        AgeInput.Foreground = Brushes.Red;
                        check = 1;
                    }

                    long creditNumber = 0;
                    if (!(long.TryParse(CreditCardInput.Text, out creditNumber) && CreditCardInput.Text.Length == 16) || creditNumber < 0)
                    {
                        CreditCardInput.Foreground = Brushes.Red;
                        check = 1;
                    }

                    
                    
                    Customer customer = null;
                    if (check == 0)
                    {
                        string gender = (string)GenderInput.SelectedValue;
                        switch (moduleSelection)
                        {
                            case ModuleSelection.General:
                                customer = new General() { FirstName = firstName, LastName = lastName, Gender= gender, PhoneNumber = phoneNumber, ModuleType = "General", Age = age, CreditCardNumber = creditNumber.ToString() };
                                if (AdditionalQuestionInput.SelectedValue.Equals("Yes"))
                                {
                                    ((General)customer).GeneralBooks = true;
                                }
                                else
                                {
                                    ((General)customer).GeneralBooks = false;
                                }
                                break;

                            case ModuleSelection.Academic:
                                customer = new Academic() { FirstName = firstName, LastName = lastName, Gender = gender, PhoneNumber = phoneNumber, ModuleType = "Academic", Age = age, CreditCardNumber = creditNumber.ToString() };
                                if (AdditionalQuestionInput.SelectedValue.Equals("Yes"))
                                {
                                    ((Academic)customer).AcademicBooks = true;
                                }

                                else
                                {
                                    ((Academic)customer).AcademicBooks = false;
                                }
                                break;
                        }

                        appArray[slot - 1].MyData = customer;
                        appointmentList.Add(appArray[slot - 1]);
                        MessageBox.Show("Added");
                        FirstNameInput.Clear();
                        LastNameInput.Clear();
                        GenderInput.SelectedIndex = -1;
                        PhoneNumberInput.Clear();
                        ModuleInput.SelectedIndex = -1;
                        AgeInput.Clear();
                        SlotInput.SelectedIndex = -1;
                        CreditCardInput.Clear();
                        AdditionalQuestionInput.SelectedIndex = -1;
                       
                    }
                }
                else
                {
                    MessageBox.Show(" Slot Taken");
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            WriteToXML(appointmentList);
            MessageBox.Show("Saved");
            SaveButton.IsEnabled = false;
            ShowButton.IsEnabled = true;
        }

        private void WriteToXML(AppointmentList list)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(AppointmentList));
            TextWriter writer = new StreamWriter("dataFile.xml");
            xmlSerializer.Serialize(writer, list);
            writer.Close();
        }

        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            ReadFromXML();
            appointmentList.Sort();
            gridDisplay.ItemsSource = appointmentList;
        }

        private void ReadFromXML()
        {
            if (File.Exists("dataFile.xml"))
            {
                string location = "dataFile.xml";

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppointmentList));

                StreamReader reader = new StreamReader(location);
                appointmentList = (AppointmentList)xmlSerializer.Deserialize(reader);
                reader.Close();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
           
            string searchSelection = (string)SearchKey.SelectedValue;

            if(searchSelection.Equals("First Name"))
            {
                string firstNameSearch = SearchInput.Text;
                bool firstNameResult = ValidateInput(firstNameSearch);
                if (firstNameResult == false)
                {
                    MessageBox.Show("Invaild Entry in search field!");
                }
                else
                {
                    var selectQuery = from list in appointmentList where list.MyData.FirstName == firstNameSearch select list;
                    gridDisplay.ItemsSource = selectQuery;
                }
            }

            if (searchSelection.Equals("Last Name"))
            {
                string lastNameSearch = SearchInput.Text;
                bool lastNameResult = ValidateInput(lastNameSearch);
                if (lastNameResult == false)
                {
                    MessageBox.Show("Invaild Entry in search field!");
                }
                else
                {
                    var selectQuery = from list in appointmentList where list.MyData.LastName == lastNameSearch select list;
                    gridDisplay.ItemsSource = selectQuery;
                }
            }
            
            if (searchSelection.Equals("Age"))
            {
                string ageSearch = SearchInput.Text;
                int age = 0;
                if (int.TryParse(ageSearch, out age))
                {
                    var selectQuery = from list in appointmentList where list.MyData.Age == age select list;
                    gridDisplay.ItemsSource = selectQuery;
                }
                else
                {
                    MessageBox.Show("Invaild Entry in search field!");
                }
            }

            if (searchSelection.Equals("Gender"))
            {
                string genderSearch = SearchInput.Text;
                bool genderResult = ValidateInput(genderSearch);
                if (genderResult == false)
                {
                    MessageBox.Show("Invaild Entry in search field!");
                }
                else
                {
                    var selectQuery = from list in appointmentList where list.MyData.Gender == genderSearch select list;
                    gridDisplay.ItemsSource = selectQuery;
                }
            }

            if (searchSelection.Equals("Module"))
            {
                string moduleSearch = SearchInput.Text;
                bool moduleResult = ValidateInput(moduleSearch);
                if (moduleResult == false)
                {
                    MessageBox.Show("Invaild Entry in search field!");
                }
                else
                {
                    var selectQuery = from list in appointmentList where list.MyData.ModuleType == moduleSearch select list;
                    gridDisplay.ItemsSource = selectQuery;
                }
            }


        }

        private void ModuleInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            moduleSelection = (ModuleSelection)ModuleInput.SelectedIndex + 1;
            switch (moduleSelection)
            {
                case ModuleSelection.General:
                    AdditionalQuestion.Content = "Do you need General exam preparation kit?";
                    AdditionalQuestionInput.Visibility = Visibility.Visible;
                    break;
                case ModuleSelection.Academic:
                    AdditionalQuestion.Content = "Do you need Academic exam preparation kit?";
                    AdditionalQuestionInput.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void NumOfMockInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SlotInput.IsEnabled = true;
            NumOfMockInput.IsEnabled = false;
            int number = NumOfMockInput.SelectedIndex + 1;
            appArray = new Appointment[number];
            DateTime theTime = DateTime.Now;
            DateTime initTime = new DateTime(theTime.Year, theTime.Month, theTime.Day, 9, 0, 0);
            for (int i = 0; i < appArray.Length; i++)
            {
                appArray[i] = new Appointment();
                appArray[i].ApptTime = initTime.ToString("HH:mm tt");
                appArray[i].SlotNumber = i + 1;
                initTime = initTime.AddHours(1);
            }
            for (int i = 0; i < appArray.Length; i++)
            {
                if (appArray[i].MyData == null)
                {
                    SlotInput.Items.Add(appArray[i].ApptTime);
                }
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            FirstNameInput.Clear();
            LastNameInput.Clear();
            GenderInput.SelectedIndex = -1;
            PhoneNumberInput.Clear();
            ModuleInput.SelectedIndex = -1;
            AgeInput.Clear();
            SlotInput.SelectedIndex = -1;
            CreditCardInput.Clear();
            AdditionalQuestionInput.SelectedIndex = -1;
            
        }

        private void SearchKey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchInput.IsEnabled = true;
        }

        private void FirstNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            FirstNameInput.Foreground = Brushes.Black;
        }

        private void LastNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            LastNameInput.Foreground = Brushes.Black;
        }

        private void AgeInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            AgeInput.Foreground = Brushes.Black;
        }

        private void PhoneNumberInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            PhoneNumberInput.Foreground = Brushes.Black;
        }

        private void CreditCardInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            CreditCardInput.Foreground = Brushes.Black;
        }
    }
}
