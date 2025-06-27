import { useState } from "react";
import SearchBar from "./components/SearchBar";
import UserList from "./components/UserList";
import UserDetails from "./components/UserDetails";

export default function App() {
  const [query, setQuery] = useState("");

  return (
    <div className="p-6 mt-3 max-w-5xl mx-auto grid md:grid-cols-2 gap-6">
      <div className="container">
        <h1 className="text-2xl font-bold mb-4">Usuarios</h1>
        <div>
          <div className="row">
            <div className="col-12 col-md-4 mb-3">
              <SearchBar query={query} setQuery={setQuery} />
            </div>
          </div>

          <div className="row">
            <div className="col-12 col-md-4 mb-3">
              <UserList filter={query} />
            </div>
          </div>
        </div>
        
        <UserDetails />
      </div>
    </div>
  );
}
