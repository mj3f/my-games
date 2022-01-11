import {useEffect, useState} from "react";

export interface ModalProps {
    title: string;
    isOpen: boolean;
    onClose: () => void;
}

const Modal: React.FC<ModalProps> = ({ isOpen, onClose, title, children }) => {
    const [isModalOpen, setIsModalOpen] = useState(false);

    useEffect(() => setIsModalOpen(isOpen), [isOpen]);

    const closeModal = () => {
        setIsModalOpen(false);
        onClose();
    }

    const display = isModalOpen ? 'container flex justify-center mx-auto' : 'hidden';
    return (
        <div className={display}>
            <div className="absolute inset-0 z-10 flex items-center justify-center bg-gray-900 bg-opacity-75">
                <div className="w-1/2 p-4 rounded bg-white">
                    <div className="flex items-center justify-between">
                        <h3 className="text-2xl font-semibold">{title}</h3>
                    </div>
                    <div className="pt-2">
                        {children}
                    </div>
                    <div className="mt-4 w-full flex justify-end">
                        <button className="w-fit p-2 bg-gray-200 rounded hover:bg-gray-300 mr-2"
                                onClick={closeModal}>Close</button>
                        <button className="w-fit p-2 bg-green-500 rounded hover:bg-green-600"
                                onClick={closeModal}>Submit</button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Modal;
