"use client";

import { createContext, useContext, useEffect, useState } from "react";
import { FilterType } from "../type";

type AppContextType = {
  fetchedData: FilterType | null;
  setFetchedData: (data: any) => void;
};

const AppContext = createContext<AppContextType>({
  fetchedData: null,
  setFetchedData: () => {},
});

export function AppWrapper({ children }: { children: React.ReactNode }) {
  const [fetchedData, setFetchedData] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      const res = await fetch("http://localhost:3000" + "/api/filters");
      const data = await res.json();
      setFetchedData(data);
    };

    if (!fetchedData) {
      fetchData();
    }
  }, [fetchedData]);
  return (
    <AppContext.Provider value={{ fetchedData, setFetchedData }}>
      {children}
    </AppContext.Provider>
  );
}

export function useAppContext() {
  return useContext(AppContext);
}
