export default function Header() {
    return (
        <div className="flex justify-between pt-2">
            <h1 className="ml-8 text-3xl">Stream Roulette</h1>
            <button
                className="rounded-full border-solid pl-2 pr-2 mr-8 bg-violet-500 hover:bg-violet-600 focus:outline-2 focus:outline-offset-2 focus:outline-violet-500 active:bg-violet-700"
                
            >
                Login
            </button>
        </div>
    );
}
