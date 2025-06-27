type Props = {
    query: string;
    setQuery: (value: string) => void;
  };
  
  export default function SearchBar({ query, setQuery }: Props) {
    return (
      <input
        type="text"
        className="form-control" 
        placeholder="Buscar usuario..."
        value={query}
        onChange={(e) => setQuery(e.target.value)}
      />
    );
  }
  