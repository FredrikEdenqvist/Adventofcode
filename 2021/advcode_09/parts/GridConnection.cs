

namespace parts
{
    public class GridConnection
    {
        public int Value { get; set; }
        public (int x, int y) Point { get; set; }
        public List<GridConnection> Connections { get; set; } = new List<GridConnection>();

        public IList<GridConnection> GetAllBut((int x, int y) p)
        {
            return Connections.Where(z => z.Point != p).ToList();
        }

        public void RemoveItem(GridConnection item)
        {
            Connections.Remove(item);
        }
    }

    public class GridClusterConnections
    {
        private HashSet<GridConnection> currentCluster;

        public GridClusterConnections()
        {
            currentCluster = new HashSet<GridConnection>();
        }

        public List<GridConnection> GetCluster(GridConnection tailThreadConnection)
        {
            if (tailThreadConnection == null)
                throw new ArgumentNullException(nameof(tailThreadConnection), "Parameter cannot be null");

            currentCluster = new HashSet<GridConnection>();

            UnRavel(tailThreadConnection);

            return currentCluster.ToList();
        }

        private void UnRavel(GridConnection tailThreadConnection)
        {
            currentCluster.Add(tailThreadConnection);
            foreach(var connection in tailThreadConnection.Connections)
            {
                connection.RemoveItem(tailThreadConnection);
            }
            var list = tailThreadConnection.Connections;
            tailThreadConnection.Connections = new List<GridConnection>();

            foreach(var connection in list)
            {
                if (currentCluster.Contains(connection))
                    continue;
                UnRavel(connection);
            }
        }
    }
}
