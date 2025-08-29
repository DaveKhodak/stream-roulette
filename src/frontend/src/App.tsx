import './App.css'
import { SpinningWheel } from './components/SpinningWheel/SpinningWheel';
import type { WheelParticipant } from './models/SpinningWheel.models';

function App() {
  const wheelParticipants: WheelParticipant[] = [
    {
      name: "1",
      value: 1
    },
    {
      name: "2",
      value: 100
    },
    {
      name: "3",
      value: 100
    }
  ]
  return (
    <>
      <SpinningWheel participants={wheelParticipants} />
    </>
  )
}

export default App
