import { useEffect, useState } from "react";

const API_BASE = "https://localhost:5278";

function App() {
  const [status, setStatus] = useState("loading...");

  useEffect(() => {.
    fetch(`${API_BASE}/api/health`)
      .then(res => res.text())
      .then(data => setStatus(data))
      .catch(err => setStatus("API ulaşılamıyor ❌"));
  }, []);

  return (
    <div style={{ padding: 40 }}>
      <h1>CreditCore UI</h1>
      <h2>{status}</h2>
    </div>
  );
}

export default App;
