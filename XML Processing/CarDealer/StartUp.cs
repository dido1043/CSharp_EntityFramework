using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using CarDealer.Utilities;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();
            //string inputXml = File.ReadAllText("../../../Datasets/sales.xml");

            var result = GetCarsWithDistance(context);
            Console.WriteLine(result);

        }
        //Import XML
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper helper = new XmlHelper();
            ImportSupplierDto[] supplierDtos =
                helper.Deserialize<ImportSupplierDto[]>(inputXml, "Suppliers")
                .ToArray();

            ICollection<Supplier> validSuppliers = new HashSet<Supplier>();
            foreach (ImportSupplierDto supplierDto in supplierDtos)
            {
                if (string.IsNullOrEmpty(supplierDto.Name))
                {
                    continue;
                }

                //Manual mapping 

                //Supplier supplier = new Supplier()
                //{ 
                //    Name = supplierDto.Name,
                //    IsImporter= supplierDto.isImporter
                //
                //};

                //Auto-mapper
                Supplier supplier = mapper.Map<Supplier>(supplierDto);
                validSuppliers.Add(supplier);

            }
            context.Suppliers.AddRange(validSuppliers);
            context.SaveChanges();
            return $"Successfully imported {validSuppliers.Count}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper helper = new XmlHelper();
            ImportPartsDto[] partsDtos =
                helper.Deserialize<ImportPartsDto[]>(inputXml, "Parts")
                .ToArray();

            ICollection<Part> parts = new HashSet<Part>();
            foreach (ImportPartsDto partsDto in partsDtos)
            {
                if (String.IsNullOrEmpty(partsDto.Name))
                    continue;

                if (!partsDto.SupplierId.HasValue ||
                    !context.Suppliers.Any(x => x.Id == partsDto.SupplierId))
                    continue;


                Part part = mapper.Map<Part>(partsDto);
                parts.Add(part);
            }
            context.Parts.AddRange(parts);
            context.SaveChanges();
            return $"Successfully imported {parts.Count}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper xmlHelper = new XmlHelper();

            ImportCarDto[] carDtos =
                xmlHelper.Deserialize<ImportCarDto[]>(inputXml, "Cars");

            ICollection<Car> validCars = new HashSet<Car>();
            foreach (ImportCarDto carDto in carDtos)
            {
                if (string.IsNullOrEmpty(carDto.Make) ||
                    string.IsNullOrEmpty(carDto.Model))
                {
                    continue;
                }

                Car car = mapper.Map<Car>(carDto);

                foreach (var partDto in carDto.Parts.DistinctBy(p => p.PartId))
                {
                    if (!context.Parts.Any(p => p.Id == partDto.PartId))
                    {
                        continue;
                    }

                    PartCar carPart = new PartCar()
                    {
                        PartId = partDto.PartId
                    };
                    car.PartsCars.Add(carPart);
                }

                validCars.Add(car);
            }

            context.Cars.AddRange(validCars);
            context.SaveChanges();

            return $"Successfully imported {validCars.Count}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper helper = new XmlHelper();
            ImportCustomerDto[] customerDtos = helper.Deserialize<ImportCustomerDto[]>(inputXml, "Customers");

            ICollection<Customer> validCustomers = new HashSet<Customer>();
            foreach (ImportCustomerDto customerDto in customerDtos)
            {
                if (String.IsNullOrEmpty(customerDto.Name))
                {
                    continue;
                }

                Customer customer = mapper.Map<Customer>(customerDto);
                validCustomers.Add(customer);
            }
            context.Customers.AddRange(validCustomers);
            context.SaveChanges();
            return $"Successfully imported {validCustomers.Count}";
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            IMapper mapper = InitializeAutoMapper();
            XmlHelper helper = new XmlHelper();
            ImportSalesDto[] salesDtos = helper.Deserialize<ImportSalesDto[]>(inputXml, "Sales");

            ICollection<Sale> validSales = new HashSet<Sale>();
            foreach (ImportSalesDto salesDto in salesDtos)
            {
                if (!context.Customers.Any(c => c.Id == salesDto.CustomerId)
                    || !context.Cars.Any(c => c.Id == salesDto.CarId))
                {
                    continue;
                }

                Sale sales = mapper.Map<Sale>(salesDto);
                validSales.Add(sales);
            }

            context.Sales.AddRange(validSales);
            context.SaveChanges();
            return $"Successfully imported {validSales.Count}";
        }


        //Export XML

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            IMapper mapper = InitializeAutoMapper();

            StringBuilder sb = new StringBuilder(); 
            ExportCarDto[] carDtos = context.Cars
                .Where(d => d.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(m => m.Model)
                .Take(10)
                .ProjectTo<ExportCarDto>(mapper.ConfigurationProvider)
                .ToArray();
            XmlHelper helper = new XmlHelper();
           return helper.Serialize<ExportCarDto[]>(carDtos,"cars");

            //For XML exporting
           // XmlRootAttribute xmlRoot = new XmlRootAttribute("cars");
           // XmlSerializer xmlSerializer =
           //     new XmlSerializer(typeof(ExportCarDto[]),xmlRoot);
           //
           // XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
           // namespaces.Add(string.Empty, string.Empty);
           //
           // using StringWriter writer = new StringWriter(sb);
           // xmlSerializer.Serialize(writer, carDtos, namespaces);   
           // return sb.ToString().TrimEnd();
        }
        private static IMapper InitializeAutoMapper()
            => new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            }));
    }


}