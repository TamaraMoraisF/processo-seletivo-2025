import DuvList from "./pages/DuvList";
import Layout from "./components/Layout";
import "./App.css";

function App() {
  return (
    <Layout>
      <div className="container">
        <DuvList />
      </div>
    </Layout>
  );
}

export default App;