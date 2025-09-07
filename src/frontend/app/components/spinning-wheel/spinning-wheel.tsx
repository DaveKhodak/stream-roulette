import { useEffect, useRef, useState } from 'react';
import { capitalize } from '../../utils/capitalize';
import confetti from 'canvas-confetti';
import type { WheelParticipant } from '../../models/spinning-wheel.models';
import * as signalR from '@microsoft/signalr';
import { getParticipants } from '~/services/spinning-wheel.service';

const colors = [
  '#CC4629', // Darker vibrant orange
  '#CC9A29', // Darker bright yellow
  '#B2CC29', // Darker light green-yellow
  '#5ECC29', // Darker bright green
  '#29CC46', // Darker bright teal-green
  '#29CC99', // Darker turquoise
  '#2985CC', // Darker sky blue
  '#293FCC', // Darker bright blue
  '#4629CC', // Darker purple
  '#9929CC', // Darker violet
  '#CC2981', // Darker hot pink
  '#CC2929', // Darker red
  '#CC5929', // Darker coral
  '#CC9529', // Darker gold
  '#B2CC29', // Darker lime green
  '#66CC29', // Darker olive green
  '#29CC5F', // Darker mint green
  '#29CC91', // Darker pale turquoise
  '#298ECC', // Darker deep sky blue
  '#4A29CC', // Darker royal blue
  '#8429CC', // Darker medium purple
  '#CC298F', // Darker fuchsia
  '#CC294F', // Darker hot pink
];

export const SpinningWheel = () => {
  const [spinning, setSpinning] = useState(false);
  const [rotation, setRotation] = useState(0);
  const [spinDirection, setSpinDirection] = useState<'clockwise' | 'counterclockwise'>('clockwise');
  const [showPopup, setShowPopup] = useState(false);
  const [popupWinner, setPopupWinner] = useState<string | null>(null);
  const [participants, setParticipants] = useState<WheelParticipant[]>([]);

  const canvasRef = useRef<HTMLCanvasElement | null>(null);
  const numSectors = participants.length;

  useEffect(() => {
    if (canvasRef.current) {
      drawWheel();
    }
  }, [participants, rotation]);

  useEffect(() => {
    getParticipants().then(response => setParticipants(response.data));
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(`${import.meta.env.VITE_API_URL}/donationHub`)
      .build();

    connection.start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));

    connection.on('ParticipantAdded', (newParticipant: WheelParticipant) => {
      setParticipants(oldParticipants => [...oldParticipants, newParticipant]);
    });
  }, [])

  const darkenColor = (color: string, amount: number): string => {
    let r = parseInt(color.slice(1, 3), 16);
    let g = parseInt(color.slice(3, 5), 16);
    let b = parseInt(color.slice(5, 7), 16);

    r = Math.max(0, r - amount);
    g = Math.max(0, g - amount);
    b = Math.max(0, b - amount);

    return `#${((1 << 24) | (r << 16) | (g << 8) | b).toString(16).slice(1)}`;
  };

  const drawWheel = () => {
    const canvas = canvasRef.current!;
    const ctx = canvas.getContext('2d')!;
    const radius = canvas.width / 2;

    // Clear previous drawing
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    ctx.translate(radius, radius);
    ctx.rotate(-rotation * (Math.PI / 180));

    const sumOfValues = participants.map(p => p.value).reduce((partialSum, a) => partialSum + a, 0);

    let offsetAngle = 0;

    // Draw sectors
    for (let i = 0; i < numSectors; i++) {
      const sliceAngle = (2 * Math.PI) * participants[i].value / sumOfValues;
      const startAngle = offsetAngle;
      const endAngle = startAngle + sliceAngle;
      offsetAngle = endAngle;
      ctx.beginPath();
      ctx.moveTo(0, 0);
      ctx.arc(0, 0, radius, startAngle, endAngle);
      ctx.closePath();
      const color = darkenColor(colors[i % colors.length], 30);
      ctx.fillStyle = color;
      ctx.fill();

      // Draw the name in the sector
      ctx.save();
      ctx.rotate((startAngle + endAngle) / 2);
      ctx.textAlign = 'center';
      ctx.textBaseline = 'middle';
      ctx.fillStyle = 'white';
      ctx.font = '16px Arial';
      ctx.shadowColor = 'rgba(0, 0, 0, 0.7)';
      ctx.shadowOffsetX = 1;
      ctx.shadowOffsetY = 1;
      ctx.shadowBlur = 3;
      ctx.fillText(capitalize(participants[i].name) || '', radius * 0.5, 0);
      ctx.restore();
    }

    ctx.rotate(rotation * (Math.PI / 180)); // Reset rotation
    ctx.translate(-radius, -radius);

    // Draw the static indicator
    const indicatorLength = 20;
    const indicatorWidth = 10;
    ctx.save();
    ctx.translate(canvas.width, canvas.height / 2);
    ctx.beginPath();
    ctx.moveTo(-indicatorLength, -indicatorWidth / 2);
    ctx.lineTo(0, -indicatorWidth / 2);
    ctx.lineTo(0, indicatorWidth / 2);
    ctx.lineTo(-indicatorLength, indicatorWidth / 2);
    ctx.closePath();
    ctx.fillStyle = 'red';
    ctx.fill();
    ctx.restore();
  };

  const startSpin = () => {
    if (spinning) return;
    setSpinning(true);

    // Set the number of full rotations and calculate final rotation
    const numFullRotations = Math.random() * 5 + 5; // Between 5 and 10 full rotations
    const totalRotation = numFullRotations * 360;
    const finalRotation =
      (rotation +
        (spinDirection === 'clockwise' ? -totalRotation : totalRotation)) %
      360;

    const spinDuration = 6000;
    const easing = (t: number) => {
      // Ease-out cubic
      return 1 - Math.pow(1 - t, 3);
    };

    let startTime: number;

    const animate = (time: number) => {
      if (!startTime) startTime = time;
      const elapsed = time - startTime;
      const t = Math.min(elapsed / spinDuration, 1);
      const easeT = easing(t);
      const currentRotation =
        rotation +
        (spinDirection === 'clockwise' ? -totalRotation : totalRotation) *
        easeT;

      setRotation(currentRotation);

      if (elapsed < spinDuration) {
        requestAnimationFrame(animate);
      } else {
        setSpinning(false);
        determineWinner(finalRotation);
      }
    };

    requestAnimationFrame(animate);
  };

  const determineWinner = (finalRotation: number) => {
    const normalizedRotation = ((finalRotation % 360) + 360) % 360;
    const sumOfValues = participants.map(p => p.value).reduce((partialSum, a) => partialSum + a, 0);
    let currentAngle = 0;
    let winningSector = 0;

    for (let i = 0; i < participants.length; i++) {
      currentAngle += 360 * participants[i].value / sumOfValues;
      if (normalizedRotation <= currentAngle) {
        winningSector = i;
        break;
      }
    }

    setPopupWinner(participants[winningSector].name);
    setShowPopup(true);
  };

  useEffect(() => {
    if (showPopup) {
      startConfetti();
      const timer = setTimeout(() => setShowPopup(false), 5000); // Hide popup after 5 seconds
      return () => clearTimeout(timer);
    }
  }, [showPopup]);

  const startConfetti = () => {
    confetti({
      particleCount: 100,
      spread: 70,
      origin: { y: 0.6 },
    });
  };

  return (
    <div className=''>
      <canvas
        ref={canvasRef}
        width={400}
        height={400}
        style={{ borderRadius: '50%', border: '2px solid black' }}
      />
      <button
        onClick={startSpin}
        disabled={participants.length === 0 || spinning}
      >
        Spin
      </button>
      {showPopup && popupWinner && (
        <div>
          <h2>Congratulations!</h2>
          <h3>{capitalize(popupWinner)}</h3>
        </div>
      )}
    </div>
  );
}