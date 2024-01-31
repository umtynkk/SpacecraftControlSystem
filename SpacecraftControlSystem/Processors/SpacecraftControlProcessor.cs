using SpacecraftControlSystem.Models;

namespace SpacecraftControlSystem.Processors
{
    public class SpacecraftControlProcessor
    {
        private SpacecraftModel _spaceCraft;
        private readonly PlanetModel _planet;

        public SpacecraftControlProcessor(SpacecraftModel spaceCraft, PlanetModel planet)
        {
            _spaceCraft = spaceCraft;
            _planet = planet;
        }

        public ResponseBase<SpacecraftModel> Move(SpacecraftControlMoveModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.MovementCommands))
            {
                foreach (char command in model.MovementCommands)
                {
                    if (command == 'L')
                    {
                        Rotate('L');
                    }
                    else if (command == 'R')
                    {
                        Rotate('R');
                    }
                    else if (command == 'M')
                    {
                        Forward();
                    }
                }

                if (_spaceCraft.PosX > _planet.MaxX)
                {
                    _spaceCraft.PosX = _planet.MaxX;
                }
                if (_spaceCraft.PosY > _planet.MaxX)
                {
                    _spaceCraft.PosY = _planet.MaxX;
                }

                return new ResponseBase<SpacecraftModel>(true, _spaceCraft);
            }

            return new ResponseBase<SpacecraftModel>(false, "Please enter a valid command!");
        }

        private ResponseBaseEmpty Rotate(char rotationDirection)
        {
            if (rotationDirection == 'L')
            {
                switch (_spaceCraft.Direction)
                {
                    case 'N':
                        _spaceCraft.Direction = 'W';
                        return new ResponseBaseEmpty(true);
                    case 'W':
                        _spaceCraft.Direction = 'S';
                        return new ResponseBaseEmpty(true);
                    case 'S':
                        _spaceCraft.Direction = 'E';
                        return new ResponseBaseEmpty(true);
                    case 'E':
                        _spaceCraft.Direction = 'N';
                        return new ResponseBaseEmpty(true);
                    default:
                        return new ResponseBaseEmpty(false, "Invalid Direction");
                }
            }
            else if (rotationDirection == 'R')
            {
                switch (_spaceCraft.Direction)
                {
                    case 'N':
                        _spaceCraft.Direction = 'E';
                        return new ResponseBaseEmpty(true);
                    case 'E':
                        _spaceCraft.Direction = 'S';
                        return new ResponseBaseEmpty(true);
                    case 'S':
                        _spaceCraft.Direction = 'W';
                        return new ResponseBaseEmpty(true);
                    case 'W':
                        _spaceCraft.Direction = 'N';
                        return new ResponseBaseEmpty(true);
                    default:
                        return new ResponseBaseEmpty(false, "Invalid Direction");
                }
            }
            else
            {
                return new ResponseBaseEmpty(false, "Invalid Direction");
            }
        }

        private ResponseBaseEmpty Forward()
        {
            switch (_spaceCraft.Direction)
            {
                case 'N':
                    _spaceCraft.PosY++;
                    return new ResponseBaseEmpty(true);
                case 'S':
                    _spaceCraft.PosY--;
                    return new ResponseBaseEmpty(true);
                case 'E':
                    _spaceCraft.PosX++;
                    return new ResponseBaseEmpty(true);
                case 'W':
                    _spaceCraft.PosX--;
                    return new ResponseBaseEmpty(true);
                default:
                    return new ResponseBaseEmpty(false, "Invalid Direction");
            }
        }
    }
}