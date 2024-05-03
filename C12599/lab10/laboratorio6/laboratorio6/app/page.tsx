'use client';
import React, { useState, useEffect } from 'react';
import { Carousel } from 'react-bootstrap';
import Link from 'next/link';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-icons/font/bootstrap-icons.css';

const Page = () => {
  const [state, setState] = useState({
    cart: {
      productos: [],
      count: 0,
    },
    productList: [], // Inicializar como un array vacío
  });

  useEffect(() => {
    const fetchData = async () => {
        const response = await fetch('https://localhost:7043/api/Store');
        if (!response.ok) {
          throw new Error('Failed to fetch data');
        }
        const json = await response.json();
        const productList = json.products || [];
        setState(prevState => ({
          ...prevState,
          productList,
        }));
    
    };

    fetchData();
  }, []);

  const handleAddToCart = (product) => {
    // Validar el objeto de producto antes de agregarlo al carrito
    if (!product || typeof product !== 'object' || !product.hasOwnProperty('id') || !product.hasOwnProperty('name') || !product.hasOwnProperty('price')) {
      throw new Error('Invalid product object');
    }

    // Validar si el producto ya está en el carrito
    const { cart } = state;
    const { productos, count } = cart;
    const isProductInCart = productos.some(item => item.id === product.id);
    if (isProductInCart) {
      throw new Error('Product is already in the cart');
    }

    // Validar el precio del producto
    if (typeof product.price !== 'number' || product.price <= 0) {
      throw new Error('Product price must be a positive number');
    }

    // Actualizar el carrito
    const updatedCart = {
      ...cart,
      productos: [...productos, product],
      count: count + 1,
    };

    setState(prevState => ({
      ...prevState,
      cart: updatedCart,
    }));

    // Guardar el carrito en localStorage
    localStorage.setItem('cartData', JSON.stringify(updatedCart));
  };

  const renderCarouselItems = (start, end) => {
    const { productList } = state;
    if (!productList || productList.length === 0) {
      return []; // Devolver un arreglo vacío si no hay productos
    }

    // Validar índices de inicio y fin
    if (typeof start !== 'number' || start < 0 || start >= productList.length ||
        typeof end !== 'number' || end <= start || end > productList.length) {
      throw new Error('Invalid start/end index for rendering carousel items');
    }

    const slicedProducts = productList.slice(start, end);
    return slicedProducts.map((product) => (
      <Carousel.Item key={product.id}>
        <div className="text-center">
          <img src={product.imageUrl} alt={product.name} style={{ maxHeight: '300px' }} />
          <h3>{product.name}</h3>
          <p>{product.description}</p>
          <p>Precio: ${product.price}</p>
          <button className="btn btn-primary" onClick={() => handleAddToCart(product)}>
            Comprar
          </button>
        </div>
      </Carousel.Item>
    ));
  };

  const renderGridItems = (start, end) => {
    const { productList } = state;
    if (!productList || productList.length === 0) {
      return []; // Devolver un arreglo vacío si no hay productos
    }

    // Validar índices de inicio y fin
    if (typeof start !== 'number' || start < 0 || start >= productList.length ||
        typeof end !== 'number' || end <= start || end > productList.length) {
      throw new Error('Invalid start/end index for rendering grid items');
    }

    const slicedProducts = productList.slice(start, end);
    return slicedProducts.map((product) => (
      <div key={product.id} className="col-sm-3 mb-4">
        <div className="card">
          <img src={product.imageUrl} className="card-img-top" alt={product.name} />
          <div className="card-body">
            <div className="text-center">
              <h5 className="card-title my-3">{product.name}</h5>
              <p className="card-text my-3">{product.description}</p>
              <p className="card-text my-3">Precio: ${product.price}</p>
              <button className="btn btn-primary" onClick={() => handleAddToCart(product)}>
                Comprar
              </button>
            </div>
          </div>
        </div>
      </div>
    ));
  };

  return (
    <div className="container">
      <div className="row">
        <div className="col-sm-8 text-center">
          <form action="/buscar" method="GET" className="mt-2">
            <input type="text" name="q" placeholder="Buscar..." />
            <button type="submit" className="btn btn-primary">
              Buscar
            </button>
          </form>
        </div>
        <div className="col-sm-2">
          <div className="my-3" style={{ position: 'relative', display: 'inline-block' }}>
            <Link href="/cart">
              <button className="btn btn-primary">
                <i className="bi bi-cart-fill"></i>
              </button>
            </Link>
            <span
              className="badge rounded-pill bg-danger"
              style={{
                position: 'absolute',
                top: '-10px',
                right: '-10px',
                fontSize: '0.8rem',
                minWidth: '20px',
                padding: '5px',
              }}
            >
              {state.cart.count}
            </span>
          </div>
        </div>
        <div className="col-sm-2">
          <div className="my-3" style={{ position: 'relative', display: 'inline-block' }}>
            <Link href="/admin">
              <button className="btn btn-primary">
                <i className="bi bi-person"></i>
              </button>
            </Link>
          </div>
        </div>
      </div>
      <div className="row">
        <h1 className="mb-0">Lista de Productos</h1>
      </div>
      <div className="row">{renderGridItems(0, 4)}</div>
      <div className="row mt-4">
        <h2 className="mb-3">Productos Destacados</h2>
        <Carousel>{renderCarouselItems(4, 8)}</Carousel>
      </div>
      <div className="row mt-4">{renderGridItems(8, 12)}</div>
      <footer className="footer mt-auto py-3" style={{ backgroundColor: '#ADD8E6' }}>
        <div className="container">
          <span className="text-muted">Mariano Duran Artavia</span>
        </div>
      </footer>
    </div>
  );
};

export default Page;
