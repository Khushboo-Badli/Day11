using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LINQ_to_XML
{
    class Program
    {
        string path = "C:\\Users\\Khushboo Badil\\source\\repos\\LINQ_to_XML\\LINQ_to_XML\\Participants.xml";
        private void GetXMLData()
        {

            try
            {
                
                //step1 connection String
                //so this is not a database so we provide path name
               
                //Step2 Loading
                XDocument doc = XDocument.Load(path); // significance of xdocument - to load xml file
                                                      // XDocument doc = XDocument.Parse(path);
                var students = from participant in doc.Descendants("Participant")
                               select new
                               {
                                   ID = Convert.ToInt32(participant.Attribute("ID").Value),
                                   Name = participant.Element("Name").Value
                               };
                Console.WriteLine("Student count " + students.Count());
                foreach (var student in students)
                {
                    Console.WriteLine(student.ID + "-" + student.Name);
                    
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        private void InsertXMLData(string name)
        {
            try
            {
                XDocument myXml = XDocument.Load(path);
                XElement newParticipant = new XElement("Participant", new XElement("Name", name));
                var LastStudent = myXml.Descendants("Participant").Last();//gets the last element
                int newID = Convert.ToInt32(LastStudent.Attribute("ID").Value);//It gets the id of last element

                newParticipant.SetAttributeValue("ID", newID+1);//entering new data
                myXml.Element("Participants").Add(newParticipant);//adding new element
                myXml.Save(path);//saving changes

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        private void ModifyXMLData(string name , int id)
        {
            try
            {
                Console.WriteLine("hi");
                XDocument modifyXML = XDocument.Load(path);
                XElement ChngParticipant = modifyXML.Descendants("Participant").Where(c => c.Attribute("ID").Value.Equals(id.ToString())).FirstOrDefault();
                
                Console.WriteLine(ChngParticipant.Attribute("ID").Value);
               ChngParticipant.Element("Name").Value = name;
               // ChngParticipant.SetAttributeValue("Name", name);
                modifyXML.Save(path);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        private void DeleteXMLData(int id)
        {
            try
            {
                XDocument testxml = XDocument.Load(path);
                XElement dparticipant = testxml.Descendants("Participant").Where(c => c.Attribute("ID").Value.Equals(id.ToString())).FirstOrDefault();
                dparticipant.Remove();
                testxml.Save(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Implementing LINQ to XML");
            Program obj1 = new Program();
           // obj1.GetXMLData();
            //obj1.InsertXMLData("Sonia");
            //obj1.GetXMLData();
            //obj1.InsertXMLData("Sharad");
            //obj1.GetXMLData();
            obj1.ModifyXMLData("Vipin", 3);
            Console.WriteLine(" After Modifying data");
            obj1.GetXMLData();
            obj1.DeleteXMLData(2);
            Console.WriteLine(" After deleting data");
            obj1.GetXMLData();

        }
    }
}
