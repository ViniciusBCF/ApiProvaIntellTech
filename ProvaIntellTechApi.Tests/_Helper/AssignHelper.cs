namespace ProvaIntellTechApi.Tests._Helper
{
    public static class AssignHelper
    {
        public static void ToProperty(
            object value,
            string attribute,
            object entity)
        {
            var property = entity.GetType().GetProperty(attribute);
            property.SetValue(entity, Convert.ChangeType(value, property.PropertyType), null);
        }
    }
}
