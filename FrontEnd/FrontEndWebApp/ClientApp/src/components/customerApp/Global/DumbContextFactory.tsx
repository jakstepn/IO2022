import React, { useState, FC, useContext, useEffect } from "react";

export interface IContext<T> {
    state: T;
    setState: (state: T) => void;
}

export type CustomStorage<T> = {
    save: (state: T) => Promise<void>;
    hydrate: () => Promise<T>;
};

export type BrowserStorage = {
    type: "session" | "local";
    key: string;
};

type Options<T> = {
    persist: BrowserStorage | CustomStorage<T> | "none";
};

type ContextState<T> = {
    state: T;
    setState: (state: T) => void;
};

export default function DumbContextFactory<T>(
    nameOfContext: string,
    defaultState: T,
    options?: Options<T>
) {
    function getDefaultState() {
        return JSON.parse(JSON.stringify(defaultState)) as T;
    }

    function getHydrate(): () => Promise<T> {
        if (!options || options?.persist === "none") {
            return () => Promise.resolve(getDefaultState());
        }
        const browserStorage = options.persist as BrowserStorage;
        if (browserStorage.type) {
            const storage = window[`${browserStorage.type}Storage`];
            return () => {
                let state: T;
                try {
                    state = JSON.parse(storage.getItem(browserStorage.key) || "") as T;
                } catch (err) {
                    state = getDefaultState();
                }
                return Promise.resolve({ ...getDefaultState(), ...state });
            };
        }
        const customStorage = options.persist as CustomStorage<T>;
        return customStorage.hydrate;
    }

    const hydrate = getHydrate();

    function getSave(): (state: T) => Promise<void> {
        if (!options || options?.persist === "none") {
            return (_state: T) => Promise.resolve();
        }
        const browserStorage = options.persist as BrowserStorage;
        if (browserStorage.type) {
            const storage = window[`${browserStorage.type}Storage`];
            return (state: T) => {
                storage.setItem(browserStorage.key, JSON.stringify(state));
                return Promise.resolve();
            };
        }
        const customStorage = options.persist as CustomStorage<T>;
        return customStorage.save;
    }

    const save = getSave();

    const DumbContext = React.createContext<ContextState<T>>({
        state: getDefaultState(),
        setState: (_state: T) => { }
    });

    const DumbContextProvider: FC = ({ children }) => {
        const [state, setState] = useState(getDefaultState());

        useEffect(() => {
            async function callHydrate() {
                const hydrated = await hydrate();
                setState(hydrated);
            }
            callHydrate();
        }, []);

        useEffect(
            function onStateChange() {
                save(state);
            },
            [state]
        );

        return (
            <DumbContext.Provider value={{ state, setState }}>
                {children}
            </DumbContext.Provider>
        );
    };

    const capitalizedName =
        nameOfContext.charAt(0).toUpperCase() + nameOfContext.slice(1);

    const useDumbContext = () => {
        const context = useContext(DumbContext);
        if (!context) {
            throw new Error(
                `use${capitalizedName} must be used in a component wrapped with ${capitalizedName}Provider`
            );
        }
        return context;
    };

    return {
        DumbContextProvider,
        useDumbContext
    };
}
